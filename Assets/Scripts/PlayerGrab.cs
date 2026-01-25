using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGrab : MonoBehaviour
{

    GameManager gameManager;
    
    //Ray from Player camera
    
    RaycastHit hit;

    GameObject grabTarget;
    GameObject heldObject;
    GameObject taskTarget;
    Rigidbody grabRb;
    Collider heldCollider;
    [SerializeField] private Pottext pottext;
    [SerializeField] private Dish dish;
    [SerializeField] private PotButton potButton;

    [SerializeField]
    public Transform handTransform;

    [SerializeField]
    private float handFollowSpeed = 1f;

    private Vector3 heldTargetNormalScale;
    Vector3 initalHandPos;

    [SerializeField] LayerMask interactable;

    [HideInInspector] public string ingredientName = null;
    private Pot lookingPot = null;
    private PotButton lookingPotButton = null;
    private void Start()
    {

        //dish.gameObject.SetActive(false);
        gameManager = FindFirstObjectByType<GameManager>();
        initalHandPos = handTransform.localPosition;

    }
    void Update()
    {
        CheckTarget();
        //MoveHand();
    }


    private Outline previousOutline;  

    // highlights observed object and targets it for grabbing
    private void CheckTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));

        if (Physics.Raycast(ray, out RaycastHit hit, 2f, interactable))
        {
            #region Set Outline

            Outline outline = hit.collider.gameObject.GetComponent<Outline>();

            if (outline != null && outline != previousOutline)
            {

                if (previousOutline != null)
                    previousOutline.enabled = false;

                outline.enabled = true;
                previousOutline = outline;
            }

            #endregion

            if (hit.collider.CompareTag("Grabbable"))
            {
                GameObject newTarget = hit.collider.gameObject;

                grabTarget = newTarget;
                return;
            }

            PotButton potButton = hit.collider.GetComponent<PotButton>();
            if (potButton != null)
            {
                taskTarget = hit.collider.gameObject;
                lookingPotButton = potButton;
                return;
            }

            Pot pot = hit.collider.GetComponent<Pot>();
            if (pot != null)
            {
                taskTarget = hit.collider.gameObject;
                lookingPot = pot;
                return;
            }

            FridgeDoor door = hit.collider.GetComponent<FridgeDoor>();
            if (door)
            {
                taskTarget = hit.collider.gameObject;
            }

            if (hit.collider.CompareTag("Task"))
            {
                taskTarget = hit.collider.gameObject;

                return;
            }
        }

        
        if (previousOutline != null)
            previousOutline.enabled = false;

        previousOutline = null;
        grabTarget = null;
        taskTarget = null;
        lookingPot = null;
    }


    private void Grab()
    {
        if (grabTarget != null && heldObject == null)
        {
            heldObject = grabTarget;
            Ingredient heldIngredients = heldObject.GetComponent<Ingredient>();
            float sizeChangeVal = heldIngredients.sizeChangeValue;
            grabRb = heldObject.GetComponent<Rigidbody>();
            //grabRb.isKinematic = true;
            //grabRb.useGravity = false;

            heldCollider = heldObject.GetComponent<Collider>();

            if (heldCollider != null)
            {
                //heldCollider.enabled = false;
                StartCoroutine(DisableCollider(grabRb, heldCollider));
            }

            Ingredient ingredient = heldObject.GetComponent<Ingredient>();
            ingredientName = ingredient.ingredientName;




            heldObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            heldObject.transform.SetParent(handTransform);
            heldObject.transform.localPosition = Vector3.zero;
            heldTargetNormalScale = heldObject.transform.localScale;
            heldObject.transform.localScale = heldObject.transform.localScale * sizeChangeVal;
            //Debug.Log(heldTargetNormalScale + ", " + heldTarget.transform.localScale);

            gameManager.AudioPlayGrab();
        }
        
    }

    private void Throw ()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;
            heldObject.transform.localScale = heldTargetNormalScale;
            grabRb.isKinematic = false;
            grabRb.useGravity = true;

            heldCollider = heldObject.GetComponent<Collider>();
            if (heldCollider != null)
            {
                heldCollider.enabled = true;
            }

            Transform cam = Camera.main.transform;
            Vector3 dropPos = cam.position + cam.forward * 1.2f;
            dropPos.y = transform.position.y + 2f;
            heldObject.transform.position = dropPos;

            StartCoroutine(Audio_CanDropCoroutine());

            heldObject = null;
            ingredientName = null;
        }
    }

    private void MoveHand()
    {
        if (heldObject == null)
        {
            handTransform.localPosition = initalHandPos;
            return;
        }

        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f);
        float mouseY = (Input.mousePosition.y / Screen.height - 0.5f);

        Vector3 targetLocalPos = new Vector3
            (
                mouseX + 1.5f,   
                mouseY - 0.5f,   
            1f               
            );

        handTransform.localPosition = Vector3.Lerp(
            handTransform.localPosition,
            targetLocalPos,
            Time.deltaTime * handFollowSpeed
        );
    }

    IEnumerator Audio_CanDropCoroutine ()
    {
        bool Is_Playing = false;
        if (Is_Playing == false)
        {
            Is_Playing = true;
            gameManager.AudioPlayCanDrop();
            yield return new WaitForSeconds(1f);
            Is_Playing = false;
        }
    }

    IEnumerator DisableCollider(Rigidbody rb, Collider coll)
    {
        yield return new WaitForFixedUpdate();

        rb.isKinematic = true;
        coll.enabled = false;
        rb.useGravity = false;

        rb.gameObject.transform.SetParent(handTransform);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed == false) return;

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
        if (dialogueManager) dialogueManager.dialogueIndex++;
        
        if (grabTarget != null)
        {
            if (heldObject != null)
            {
                Throw(); 
            }
            Grab();
            return;
        }

        if (lookingPot != null)
        {
            if (heldObject != null)
            {
                lookingPot.PutIngredient(heldObject);
                heldObject = null;
                ingredientName = null;
            }
            if (heldObject == null)
            {                
                pottext.IngredientsPutTextZero();
            }
        }
        
        if (lookingPotButton != null)
        {
            pottext.MakeFoodText();
            potButton.MakeDish();
            dish.gameObject.SetActive(true);
            Debug.Log(dish.dishType);
        }

        if (taskTarget != null && heldObject != null && heldObject.GetComponent<Ingredient>() != null && heldObject.GetComponent<Ingredient>().canBeCut)
        {
            CuttingBoard board = taskTarget.GetComponent<CuttingBoard>();

            if (board != null)
            {
                board.CutIngredient(heldObject);
                heldObject = null;
                ingredientName = null;
                return;
            }
        }

        if(taskTarget != null && taskTarget.GetComponent<Bell>() != null)
        {
            taskTarget.GetComponent<Bell>().Ring();
        }

        if(taskTarget != null && taskTarget.GetComponent<FridgeDoor>() != null)
        {
            taskTarget.GetComponent<FridgeDoor>().ToggleDoor();
        }

        if (heldObject != null)
        {
            Throw();
        }
    }
}
