using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MapRaycaster : MonoBehaviour
{
    [Header("Raycast Configurations")]
    [SerializeField] private Camera targetCamera;
    [SerializeField] private LayerMask mapLayerMask;

    [Header("UI Hover Integration")]
    [SerializeField] private TextMeshProUGUI hoverTooltipText;

    private void Awake()
    {
        // Fallback safety if camera is not assigned in the inspector
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void Update()
    {
        // 1. Process Click Inputs
        if (Pointer.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            PerformMapLookup(true);
        }
        // 2. Continuous Hover Input tracking
        else
        {
            PerformMapLookup(false);
        }
    }

    private void PerformMapLookup(bool isExplicitClick)
    {
        if (Pointer.current == null) return;

        Vector2 mousePosition = Pointer.current.position.ReadValue();
        Ray ray = targetCamera.ScreenPointToRay(mousePosition);

        // Perform optimized physics calculation using our custom layer mask
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mapLayerMask))
        {
            Renderer mapRenderer = hit.collider.GetComponent<Renderer>();
            if (mapRenderer == null) return;

            Texture2D lookupTexture = mapRenderer.sharedMaterial.mainTexture as Texture2D;
            if (lookupTexture == null) return;

            Vector2 uv = hit.textureCoord;

            int pixelX = Mathf.FloorToInt(uv.x * lookupTexture.width);
            int pixelY = Mathf.FloorToInt(uv.y * lookupTexture.height);

            Color clickedColor = lookupTexture.GetPixel(pixelX, pixelY);

            if (isExplicitClick)
            {
                // ROUTE CLICK EVENT: Send full data profile packet to our primary info layout
                if (MapManager.Instance != null)
                {
                    MapManager.Instance.GetProvinceFromColor(clickedColor);
                }
            }
            else
            {
                // ROUTE HOVER EVENT: Ask the manager if a valid province sits under this color key
                ProcessHoverContext(clickedColor);
            }
        }
        else if (!isExplicitClick)
        {
            // Clear hover text if mouse flies completely off the map mesh surface geometry
            ClearHoverDisplay();
        }
    }

    private void ProcessHoverContext(Color hoverColor)
    {
        if (MapManager.Instance == null || hoverTooltipText == null) return;

        Color32 targetKey = hoverColor;
        
        // Update data type to point to our newly decoupled runtime state signature wrapper card
        RuntimeProvinceState hoveredState = ExtractProvinceFromRegistryDirectly(targetKey);

        if (hoveredState != null)
        {
            hoverTooltipText.text = $"<color=yellow>{hoveredState.SourceAsset.provinceName}</color>";
        }
        else
        {
            ClearHoverDisplay();
        }
    }

    // Adjust return identifier types cleanly
    private RuntimeProvinceState ExtractProvinceFromRegistryDirectly(Color32 colorKey)
    {
        if (MapManager.Instance != null)
        {
            return MapManager.Instance.GetProvinceDataRaw(colorKey);
        }
        return null;
    }

    private void ClearHoverDisplay()
    {
        if (hoverTooltipText != null)
        {
            hoverTooltipText.text = "";
        }
    }

    // Quick helper to read data silently without executing click logs
    private ProvinceData ExtractProvinceFromRegistryDirectly(Color32 colorKey)
    {
        if (MapManager.Instance != null)
        {
            return MapManager.Instance.GetProvinceDataRaw(colorKey);
        }
        return null;
    }
}