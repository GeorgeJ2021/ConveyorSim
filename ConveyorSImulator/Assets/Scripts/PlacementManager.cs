using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;

    [Header("Prefabs")]
    [SerializeField] private GameObject shortConveyor;
    [SerializeField] private GameObject stdConveyor;
    [SerializeField] private GameObject inclineConveyor;

    [SerializeField]private float placementOffset = 0;
    private float currentRotation = 0f;

    private GameObject currentGhost;
    private GameObject currentPrefab;

    public void SetCurrentPrefab(GameObject prefab)
    {
        currentPrefab = prefab;

        if(currentGhost != null)
            Destroy(currentGhost);

        currentGhost = Instantiate(currentPrefab);
        currentGhost.name = "Ghost";
    }
    
    private void MoveGhost()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            currentRotation += 90f;

            if (currentRotation >= 360f)
                currentRotation = 0f;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
           Vector3 position = hit.point;
           position.y += placementOffset;
           currentGhost.transform.position = position;
           currentGhost.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }
    }
    private void Update()
    {
        if (currentGhost == null)
            return;

        MoveGhost();
    }

    public void SelectShortConveyor()
    {
        SetCurrentPrefab(shortConveyor);
        placementOffset = 0.44f;
    }

    public void SelectSTDConveyor()
    {
        SetCurrentPrefab(stdConveyor);
        placementOffset = 0.44f;
        
    }

    public void SelectInclineConveyor()
    {
        SetCurrentPrefab(inclineConveyor);
        placementOffset = 0.8f;
    }


}
