using UnityEngine;

public class Product : MonoBehaviour
{
    private conveyor currentConveyor;

    private float distanceTravelled;

    [Header("Settings")]
    public bool FollowConveyorRotation = false;
    [SerializeField]
    private Vector3 positionOffset;

    [SerializeField] private LayerMask productLayer;
    private Collider productCollider;

    private void Awake()
    {
        productCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        distanceTravelled = 0f;
    }

    private void Update()
    {
        if (currentConveyor == null)
            return;

        float proposedDistance =
            distanceTravelled +
            currentConveyor.speed * Time.deltaTime;

        float progress =
            Mathf.Clamp01(proposedDistance / currentConveyor.Length);

        Vector3 nextPosition =
            currentConveyor.GetPosition(progress) +
            positionOffset;

        Bounds bounds = productCollider.bounds;

        Vector3 overlapCentre =
            nextPosition +
            (bounds.center - transform.position);

        Collider[] hits = Physics.OverlapBox(
            overlapCentre,
            bounds.extents * 0.9f,
            transform.rotation,
            productLayer);

        bool blocked = false;

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            blocked = true;
            break;
        }

        if (!blocked)
        {
            distanceTravelled = proposedDistance;
            transform.position = nextPosition;

            if (FollowConveyorRotation)
            {
                transform.rotation =
                    currentConveyor.GetRotation();
            }
        }

        float actualProgress =
            distanceTravelled /
            currentConveyor.Length;

        if (actualProgress >= 1f)
        {
            if (currentConveyor.NextConveyor != null)
            {
                currentConveyor =
                    currentConveyor.NextConveyor;

                distanceTravelled = 0f;
            }
            else
            {
                enabled = false;
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
