using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private NativeInput input;

    private void OnEnable()
    {
        input = new NativeInput();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public Vector2 GetMovementInput()
    {
        return input.Gameplay.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPointerPosition()
    {
        Vector3 pointerPosition = input.Gameplay.Pointer.ReadValue<Vector2>();
        pointerPosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(pointerPosition);
    }

    public bool GetDash()
    {
        return input.Gameplay.Dash.WasPressedThisFrame();
    }

    public bool GetRoll()
    {
        return input.Gameplay.Roll.WasPressedThisFrame();
    }
}