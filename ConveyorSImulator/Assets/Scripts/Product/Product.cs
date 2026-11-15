using UnityEngine;

public class Product : MonoBehaviour
{
    private conveyor currentConveyor;

    private float distanceTravelled;

    [Header("Settings")]
    public bool FollowConveyorRotation = false;
    [SerializeField]
    private Vector3 positionOffset;

    private void Start()
    {
        distanceTravelled = 0f;
    }

    private void Update()
    {
        if (currentConveyor == null)
            return;

        distanceTravelled += currentConveyor.speed * Time.deltaTime;

        float progress = distanceTravelled / currentConveyor.Length;

        progress = Mathf.Clamp01(progress);

        transform.position = currentConveyor.GetPosition(progress) + positionOffset;

        if (FollowConveyorRotation)
        {
            transform.rotation = currentConveyor.GetRotation();
        }

        if (progress >= 1f)
        {
            if (currentConveyor.NextConveyor != null)
            {
               currentConveyor = currentConveyor.NextConveyor;
               distanceTravelled = 0f;
               Debug.Log("Reached next conveyor.");
            }
            else
            {
                enabled = false;
                Debug.Log("Reached end of conveyor.");
            }
            

            
        }
    }

    public void SetCurrentConveyor(conveyor conveyor)
    {
        currentConveyor = conveyor;
        distanceTravelled = 0f;

        transform.position = conveyor.GetPosition(0f) + positionOffset;

        if (FollowConveyorRotation)
        {
            transform.rotation = conveyor.GetRotation();
        }
    }
}
