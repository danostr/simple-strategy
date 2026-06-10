using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Singleton Instance so our Raycaster can find it instantly
    public static MapManager Instance { get; private set; }

    [Header("Database Configuration")]
    [SerializeField] private List<ProvinceData> allProvinces = new List<ProvinceData>();

    // High-speed runtime lookup map linking Color keys to Province Data
    private Dictionary<Color32, ProvinceData> provinceColorRegistry = new Dictionary<Color32, ProvinceData>();

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
        foreach (ProvinceData province in allProvinces)
        {
            if (province == null) continue;

            // Using Color32 prevents floating-point precision mismatch issues during lookups
            Color32 keyColor = province.colorIdentity;

            if (!provinceColorRegistry.ContainsKey(keyColor))
            {
                provinceColorRegistry.Add(keyColor, province);
                Debug.Log($"<color=cyan><b>[Map Manager]</b></color> Registered '[ID: {province.provinceID}] {province.provinceName}' under color key: {keyColor}");
            }
            else
            {
                Debug.LogWarning($"[Map Manager] Duplicate color identity detected for {province.provinceName}!");
            }
        }
    }

    public void GetProvinceFromColor(Color clickedColor)
    {
        Color32 targetKey = clickedColor;

        if (provinceColorRegistry.TryGetValue(targetKey, out ProvinceData foundProvince))
        {
            Debug.Log($"<color=yellow><b>[Map Manager]</b></color> Selected Territory: <b>{foundProvince.provinceName}</b> (ID: {foundProvince.provinceID})");
        }
        else
        {
            Debug.LogWarning($"[Map Manager] No territory found matching color signature: {targetKey}");
        }
    }
}
