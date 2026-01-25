using Unity.VisualScripting;
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
    [SerializeField] private GameObject pauseMenu;

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

    public void OnOpenBook(InputAction.CallbackContext context)
    { 
        if (context.performed)
        {
            ToggleBook();
        }
    }

    public void OnPauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            fpController.PausePlayer();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
            Time.timeScale = 1f;
            if (!dialogueManager.inDialogue) fpController.ResumePlayer();
            Cursor.lockState = CursorLockMode.Locked;
        }

        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    private void Start()
    {
        if (Book != null) Book.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(false);

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

    private void ToggleBook()
    {
        isLookingAtBook = !isLookingAtBook;

        Book.SetActive(isLookingAtBook);

        
        fpController.canLook = !isLookingAtBook;
        fpController.canMove = !isLookingAtBook;


        //playerInput.enabled = !isLookingAtBook;
        if (isLookingAtBook)
        {
            fpController.PausePlayer();
        }
        else
        {
            fpController.ResumePlayer();
        }
        
        Cursor.lockState = isLookingAtBook
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = isLookingAtBook;
    }
}
