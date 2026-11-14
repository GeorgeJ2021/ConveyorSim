using UnityEngine;
using UnityEngine.InputSystem;


public class CameraCTRL : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float fastMoveMultiplier = 3f;

    [Header("Rotation")]
    public float rotationSpeed = 150f;
    public float minPitch = 20f;
    public float maxPitch = 80f;

    [Header("Zoom")]
    public float zoomSpeed = 20f;
    public float minZoom = 5f;
    public float maxZoom = 50f;

    public Transform cameraTransform;

    private float pitch = 45f;
    private float yaw = 0f;
    private float zoomDistance = 20f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;

        yaw = angles.y;
        pitch = angles.x;

        zoomDistance = 
            cameraTransform.localPosition.magnitude;
    }
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }


    void HandleMovement()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y += 1;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        Vector3 direction =
            forward * input.y +
            right * input.x;
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        float speed = moveSpeed;

        if (Keyboard.current.leftShiftKey.isPressed)
            speed *= fastMoveMultiplier;

        transform.position +=
            direction *
            speed *
            Time.deltaTime;
    }

    void HandleRotation()
    {
        if(Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta =
                Mouse.current.delta.ReadValue();

            yaw += mouseDelta.x * rotationSpeed * Time.deltaTime;

            pitch -= mouseDelta.y * rotationSpeed * Time.deltaTime;

            pitch =
                Mathf.Clamp(
                    pitch,
                    minPitch,
                    maxPitch);

            transform.rotation =
                Quaternion.Euler(
                    pitch,
                    yaw,
                    0);
        }
    }


    void HandleZoom()
    {
        float scroll =
            Mouse.current.scroll.ReadValue().y;

        zoomDistance -= 
            scroll * zoomSpeed * Time.deltaTime;

        zoomDistance =
            Mathf.Clamp(
                zoomDistance,
                minZoom,
                maxZoom);

        cameraTransform.localPosition =
            new Vector3(
                0,
                0,
                -zoomDistance);
    }
}
