using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FPController fpController;
    [SerializeField] private PlayerInput playerInput;

    [Header("UI")]
    [SerializeField] private GameObject Book;

    private bool isLookingAtBook = false;

    // ===== Input System =====
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!fpController.canMove) return;
        fpController.moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!fpController.canLook) return;
        fpController.lookInput = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        Book.SetActive(false);

        fpController.canLook = true;
        fpController.canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnValidate()
    {
        if (fpController == null) fpController = GetComponent<FPController>();
        if (playerInput == null) playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleBook();
        }
    }

    private void ToggleBook()
    {
        isLookingAtBook = !isLookingAtBook;

        Book.SetActive(isLookingAtBook);

        
        fpController.canLook = !isLookingAtBook;
        fpController.canMove = !isLookingAtBook;

        
        playerInput.enabled = !isLookingAtBook;

        
        Cursor.lockState = isLookingAtBook
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = isLookingAtBook;
    }
}
