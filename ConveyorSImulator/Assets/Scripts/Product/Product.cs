using UnityEngine;

public class Product : MonoBehaviour
{
    public conveyor currentConveyor;

    private float distanceTravelled;

    public bool FollowConveyorRotation = false;

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

        transform.position = currentConveyor.GetPosition(progress);

        if (FollowConveyorRotation)
        {
            transform.rotation = currentConveyor.GetRotation();
        }

        if (progress >= 1f)
        {
            enabled = false;

            Debug.Log("Reached end of conveyor.");
        }
    }
}
