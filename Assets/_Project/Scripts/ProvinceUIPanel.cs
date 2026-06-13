using UnityEngine;
using TMPro;

public class ProvinceUIPanel : MonoBehaviour
{
    [Header("UI Readout Components")]
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI populationText;
    [SerializeField] private TextMeshProUGUI economyText;

    private void Awake()
    {
        // Default the panel to a hidden state on startup
        HidePanel();
    }

    /// <summary>
    /// Public entry point to map incoming data variables and toggle visibility on.
    /// </summary>
    public void DisplayProvince(RuntimeProvinceState dataState)
    {
        if (data == null)
        {
            HidePanel();
            return;
        }

        // Map data directly to string outputs
        idText.text = $"ID: {data.provinceID}";
        nameText.text = $"Region: {data.provinceName}";
        populationText.text = $"Population: {data.population:N0}"; // Formats 45000 as 45,000
        economyText.text = $"Economy Baseline: {data.economy}";

        // Make the panel overlay active on screen
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Clears information and hides the panel frame.
    /// </summary>
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}