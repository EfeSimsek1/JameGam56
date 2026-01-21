using NUnit.Framework.Constraints;
using System.Collections;
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

    [SerializeField]
    public Transform handTransform;

    [SerializeField]
    private float handFollowSpeed = 1f;

    private Vector3 heldTargetNormalScale;
    Vector3 initalHandPos;

    private void Start()
    {
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

        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
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

        
        if (grabTarget != null)
        {
            if (heldObject != null)
            {
                Throw(); 
            }
            Grab();
            return;
        }

        if (taskTarget != null && heldObject.GetComponent<Ingredient>().canBeCut)
        {
            CuttingBoard board = taskTarget.GetComponent<CuttingBoard>();

            if (board != null)
            {
                board.CutIngredient(heldObject);
                heldObject = null;
                return;
            }
        }

        if (heldObject != null)
        {
            Throw();
        }
    }



}
