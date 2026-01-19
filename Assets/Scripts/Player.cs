using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPController))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] FPController fpController;

    void OnMove(InputValue value)
    {
        fpController.moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        fpController.lookInput = value.Get<Vector2>();
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
