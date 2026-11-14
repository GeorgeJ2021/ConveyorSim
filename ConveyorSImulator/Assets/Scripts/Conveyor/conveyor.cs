using UnityEngine;

public class conveyor : MonoBehaviour
{
    [Header("Conveyor Path")]
    public Transform entryPoint;
    public Transform exitPoint;

    [Header("Connections")]
    [SerializeField]private conveyor previousConveyor;
    public conveyor PreviousConveyor => previousConveyor;
    [SerializeField]private conveyor nextConveyor;
    public conveyor NextConveyor => nextConveyor;

    [Header("Settings")]
    public float speed = 2f;

    public Vector3 GetPosition(float progress)
    {
        return Vector3.Lerp(
            entryPoint.position,
            exitPoint.position,
            progress);
    }

    public Quaternion GetRotation()
    {
        Vector3 direction = (exitPoint.position - entryPoint.position).normalized;

        if (direction == Vector3.zero)
            return transform.rotation;

        return Quaternion.LookRotation(direction);
    }

    public float Length
    {
        get
        {
            return Vector3.Distance(entryPoint.position, exitPoint.position);
        }
    }
}
