using UnityEngine;
using UnityEngine.InputSystem;

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
        // 2. Read the primary click using the new Input System API
        if (Pointer.current != null && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            PerformMapLookup();
        }
    }

    private void PerformMapLookup()
    {
        // 3. Read the cursor position using the new modern vector tracking
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        Ray ray = targetCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mapLayer))
        {
            Renderer meshRenderer = hit.collider.GetComponent<Renderer>();
            if (meshRenderer == null || meshRenderer.sharedMaterial == null) return;

            Texture2D lookupTexture = meshRenderer.sharedMaterial.mainTexture as Texture2D;
            if (lookupTexture == null) return;

            Vector2 uv = hit.textureCoord;

            int pixelX = Mathf.FloorToInt(uv.x * lookupTexture.width);
            int pixelY = Mathf.FloorToInt(uv.y * lookupTexture.height);

            Color clickedColor = lookupTexture.GetPixel(pixelX, pixelY);

            Debug.Log($"<color=green><b>[Map Raycaster]</b></color> Sampled Color: {clickedColor} at UV ({uv.x:F3}, {uv.y:F3})");
        }
    }
}