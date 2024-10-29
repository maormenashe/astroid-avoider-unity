using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;

    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 movementDirection;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
        WrapPlayerOnScreen();
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero)
        {
            return;
        }

        rb.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
    }

    private void ProcessInput()
    {
        TouchControl primaryTouch = Touchscreen.current.primaryTouch;

        if (!primaryTouch.press.isPressed)
        {
            movementDirection = Vector3.zero;
            return;
        }

        Vector2 touchPosition = primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        movementDirection = transform.position - worldPosition;
        movementDirection.z = 0;
        movementDirection.Normalize();
    }

    private void WrapPlayerOnScreen()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x > 1) viewportPosition.x -= 1;
        else if (viewportPosition.x < 0) viewportPosition.x += 1;

        if (viewportPosition.y > 1) viewportPosition.y -= 1;
        else if (viewportPosition.y < 0) viewportPosition.y += 1;

        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }
}
