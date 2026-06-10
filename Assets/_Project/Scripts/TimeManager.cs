using UnityEngine;
using UnityEngine.InputSystem;

public enum ClockSpeed { Paused = 0, Normal = 1, Fast = 2 }

public class TimeManager : MonoBehaviour
{
    // Primary Singleton Accessor
    public static TimeManager Instance { get; private set; }

    // Primary Monthly Event Dispatcher
    public event System.Action OnMonthTick;

    [Header("Simulation Timing Settings")]
    [Tooltip("How many real-time seconds it takes for 1 game day to pass at Normal speed.")]
    [SerializeField] private float realSecondsPerGameDay = 1.0f;

    [Header("Current Calendar State")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int currentMonth = 1; // 1 = January, 12 = December
    [SerializeField] private int currentYear = 1444;

    private ClockSpeed currentSpeed = ClockSpeed.Paused;
    private ClockSpeed prePauseSpeed = ClockSpeed.Normal; // Remembers last speed when hitting spacebar
    private float dayAccumulator = 0.0f;

    // Fixed array representing days in each month (Index 0 = Jan, Index 11 = Dec)
    private readonly int[] daysInMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    
    // Month name strings for UI text processing later
    private readonly string[] monthNames = { 
        "January", "February", "March", "April", "May", "June", 
        "July", "August", "September", "October", "November", "December" 
    };

    // Public Getters for UI layers and external engines
    public int CurrentDay => currentDay;
    public int CurrentYear => currentYear;
    public ClockSpeed CurrentSpeed => currentSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        // Handle Global Input shortcut: Spacebar toggles pause/unpause state
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TogglePause();
        }

        // Advance simulation clock if game is unpaused
        if (currentSpeed != ClockSpeed.Paused)
        {
            ProcessTimePassing();
        }
    }

    private void ProcessTimePassing()
    {
        // Speed scale modifier: Normal = 1x speed, Fast = 2x speed
        float speedModifier = (float)currentSpeed;

        // Frame-rate independent tracking accumulation
        dayAccumulator += Time.deltaTime * speedModifier;

        // If enough real time has passed to fill a complete game day
        if (dayAccumulator >= realSecondsPerGameDay)
        {
            dayAccumulator -= realSecondsPerGameDay;
            AdvanceDay();
        }
    }

    private void AdvanceDay()
    {
        currentDay++;

        // Read day limit constraints for the active month (ignoring leap years for simplicity)
        int maxDaysInCurrentMonth = daysInMonths[currentMonth - 1];

        if (currentDay > maxDaysInCurrentMonth)
        {
            currentDay = 1;
            AdvanceMonth();
        }
    }

    private void AdvanceMonth()
    {
        currentMonth++;

        if (currentMonth > 12)
        {
            currentMonth = 1;
            currentYear++;
        }

        Debug.Log($"<color=orange><b>[Simulation Clock]</b></color> Advanced to: {GetFormattedDateString()}");

        // Broadcast the Month Tick out to the world!
        OnMonthTick?.Invoke();
    }

    /// <summary>
    /// Translates internal numbers into an immersive date string layout.
    /// </summary>
    public string GetFormattedDateString()
    {
        string monthText = monthNames[currentMonth - 1];
        return $"{monthText} {currentDay}, {currentYear}";
    }

    #region Simulation Time Speed Modifiers
    public void SetSpeed(ClockSpeed targetSpeed)
    {
        currentSpeed = targetSpeed;
        if (targetSpeed != ClockSpeed.Paused)
        {
            prePauseSpeed = targetSpeed;
        }
        Debug.Log($"[Simulation Clock] Speed updated to: {currentSpeed}");
    }

    public void TogglePause()
    {
        if (currentSpeed == ClockSpeed.Paused)
        {
            SetSpeed(prePauseSpeed);
        }
        else
        {
            prePauseSpeed = currentSpeed;
            SetSpeed(ClockSpeed.Paused);
        }
    }
    #endregion
}