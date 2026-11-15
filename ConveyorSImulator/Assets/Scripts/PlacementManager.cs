using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float snapDistance = 0.3f;

    [Header("Prefabs")]
    private readonly List<conveyor> placedConveyors = new();
    [SerializeField] private GameObject shortConveyor;
    [SerializeField] private GameObject stdConveyor;
    [SerializeField] private GameObject inclineConveyor;

    [SerializeField]private float placementOffset = 0;
    private float currentRotation = 0f;

    private GameObject currentGhost;
    private GameObject currentPrefab;
    private conveyor snappedTo;

    public void SetCurrentPrefab(GameObject prefab)
    {
        currentPrefab = prefab;

        if(currentGhost != null)
            Destroy(currentGhost);

        currentGhost = Instantiate(currentPrefab);
        currentGhost.name = "Ghost";
    }
    
    private void TrySnap(ref Vector3 targetPosition)
    {
        conveyor ghostConveyor = currentGhost.GetComponent<conveyor>();
        conveyor closestConveyor = null;
        snappedTo = null;
        
        float closestDistance = Mathf.Infinity;
        
        Vector3 localOffset = ghostConveyor.entryPoint.localPosition;

         Vector3 ghostEntry =
                targetPosition +
                (currentGhost.transform.rotation * localOffset);

        foreach (conveyor Conveyor in placedConveyors)
        {
            //if (Conveyor.gameObject == ghostObject)
              //  continue;
            
            float distance =
                Vector3.Distance(
                    ghostEntry,
                    Conveyor.exitPoint.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestConveyor = Conveyor;
            }

            if (closestConveyor != null && closestDistance < snapDistance)
            {
                Vector3 offset =
                    Conveyor.exitPoint.position -
                    ghostEntry;

                targetPosition += offset;
                snappedTo = closestConveyor;

                break;
            }
        }
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
           Vector3 targetPosition = hit.point;
           targetPosition.y += placementOffset;
           currentGhost.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
           TrySnap(ref targetPosition);
           currentGhost.transform.position = targetPosition;
           
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && 
            !EventSystem.current.IsPointerOverGameObject())
        {
            GameObject conveyorObject = Instantiate(
                currentPrefab,
                currentGhost.transform.position,
                currentGhost.transform.rotation);

            conveyor Conveyor = conveyorObject.GetComponent<conveyor>();

            placedConveyors.Add(Conveyor);
            if (snappedTo != null)
            {
                snappedTo.SetNextConveyor(Conveyor);
                Conveyor.SetPreviousConveyor(snappedTo);
            }
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
        placementOffset = 0.1f;
        
    }

    public void SelectInclineConveyor()
    {
        SetCurrentPrefab(inclineConveyor);
        placementOffset = 0.8f;
    }


}
