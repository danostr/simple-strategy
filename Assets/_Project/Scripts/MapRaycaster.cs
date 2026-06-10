using UnityEngine;

public class MapRaycaster : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Camera targetCamera;
    [SerializeField] private LayerMask mapLayer;

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
        // 1. Listen for the primary left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            PerformMapLookup();
        }
    }

    private void PerformMapLookup()
    {
        // 2. Convert the mouse screen position into a physical 3D ray vector
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 3. Fire the raycast targeting our specific map layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mapLayer))
        {
            // 4. Validate that the hit object has a Renderer component
            Renderer meshRenderer = hit.collider.GetComponent<Renderer>();
            if (meshRenderer == null || meshRenderer.sharedMaterial == null) return;

            // 5. Extract the main texture assigned to the renderer's material
            Texture2D lookupTexture = meshRenderer.sharedMaterial.mainTexture as Texture2D;
            if (lookupTexture == null)
            {
                Debug.LogWarning("Map Raycaster: The hit target's material does not have a 2D Texture assigned as its Main Texture.");
                return;
            }

            // 6. Retrieve the mathematical UV coordinate of the collision point
            Vector2 uv = hit.textureCoord;

            // 7. Convert the normalized UV coordinate (0.0 to 1.0) into absolute pixel coordinates
            int pixelX = Mathf.FloorToInt(uv.x * lookupTexture.width);
            int pixelY = Mathf.FloorToInt(uv.y * lookupTexture.height);

            // 8. Sample the precise pixel color on the hardware asset
            Color clickedColor = lookupTexture.GetPixel(pixelX, pixelY);

            // 9. Output to the console console to verify accuracy
            Debug.Log($"<color=green><b>[Map Raycaster]</b></color> Sampled Color: {clickedColor} at UV ({uv.x:F3}, {uv.y:F3})");
        }
    }
}