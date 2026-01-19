using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPController))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] FPController fpController;

    public void OnMove(InputAction.CallbackContext context)
    {
        fpController.moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        fpController.lookInput = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnValidate()
    {
        if (fpController == null) fpController = GetComponent<FPController>();
    }
}
