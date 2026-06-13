using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [Header("Database Configuration")]
    [SerializeField] private List<ProvinceData> allProvinces = new List<ProvinceData>();

    [Header("UI Integration")]
    [SerializeField] private ProvinceUIPanel provinceUIPanel;

    // Rerouted dictionary mapping Color32 keys straight to live decoupled memory wrappers
    private Dictionary<Color32, RuntimeProvinceState> provinceColorRegistry = new Dictionary<Color32, RuntimeProvinceState>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeColorRegistry();
    }

    private void InitializeColorRegistry()
    {
        foreach (ProvinceData assetTemplate in allProvinces)
        {
            if (assetTemplate == null) continue;

            Color32 keyColor = assetTemplate.colorIdentity;

            if (!provinceColorRegistry.ContainsKey(keyColor))
            {
                // DECOUPLE LOGIC: Wrap the static file template into a fresh runtime instance state
                RuntimeProvinceState liveStateInstance = new RuntimeProvinceState(assetTemplate);
                
                provinceColorRegistry.Add(keyColor, liveStateInstance);
                Debug.Log($"<color=cyan><b>[Map Manager]</b></color> Instantiated decoupled runtime state wrapper for '[ID: {assetTemplate.provinceID}] {assetTemplate.provinceName}'");
            }
            else
            {
                Debug.LogWarning($"[Map Manager] Duplicate color identity detected for {assetTemplate.provinceName}!");
            }
        }
    }

    public void GetProvinceFromColor(Color clickedColor)
    {
        Color32 targetKey = clickedColor;

        // Query the runtime live database registry map
        if (provinceColorRegistry.TryGetValue(targetKey, out RuntimeProvinceState foundState))
        {
            Debug.Log($"<color=yellow><b>[Map Manager]</b></color> Selected Runtime Territory: <b>{foundState.SourceAsset.provinceName}</b>");
            
            if (provinceUIPanel != null)
            {
                provinceUIPanel.DisplayProvince(foundState);
            }
        }
        else
        {
            Debug.LogWarning($"[Map Manager] No territory found matching color signature: {targetKey}");
            if (provinceUIPanel != null)
            {
                provinceUIPanel.HidePanel();
            }
        }
    }

    /// <summary>
    /// Public tool allowing hover tracking systems to safely poll regional records silently.
    /// </summary>
    public RuntimeProvinceState GetProvinceDataRaw(Color32 colorKey)
    {
        if (provinceColorRegistry.TryGetValue(colorKey, out RuntimeProvinceState dataState))
        {
            return dataState;
        }
        return null;
    }
}