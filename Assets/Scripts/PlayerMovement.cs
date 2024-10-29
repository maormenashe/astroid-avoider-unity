using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        TouchControl primaryTouch = Touchscreen.current.primaryTouch;

        if (!primaryTouch.press.isPressed)
        {
            return;
        }

        Vector2 touchPosition = primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        Debug.Log($"{touchPosition} {worldPosition}");
    }
}
