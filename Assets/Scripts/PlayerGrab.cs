using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{

    GameManager gameManager;
    
    //Ray from Player camera
    
    RaycastHit hit;

    GameObject grabTarget;
    GameObject heldTarget;
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
        Debug.DrawRay(ray.origin, ray.direction * 4f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                
                GameObject newTarget = hit.collider.gameObject;
                Outline outline = newTarget.GetComponent<Outline>();

                if (outline != null && outline != previousOutline)
                {
                    
                    if (previousOutline != null)
                        previousOutline.enabled = false;

                    outline.enabled = true;
                    previousOutline = outline;
                }

                grabTarget = newTarget;
                return;
            }
        }

        
        if (previousOutline != null)
            previousOutline.enabled = false;

        previousOutline = null;
        grabTarget = null;
    }


    private void Grab()
    {
        if (grabTarget != null && heldTarget == null)
        {
            heldTarget = grabTarget;
            Ingredient heldIngredients = heldTarget.GetComponent<Ingredient>();
            float sizeChangeVal = heldIngredients.sizeChangeValue;
            grabRb = heldTarget.GetComponent<Rigidbody>();
            grabRb.isKinematic = true;
            grabRb.useGravity = false;

            heldCollider = heldTarget.GetComponent<Collider>();
            if (heldCollider != null)
                heldCollider.enabled = false;

            heldTarget.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            heldTarget.transform.SetParent(handTransform);
            heldTarget.transform.localPosition = Vector3.zero;
            heldTargetNormalScale = heldTarget.transform.localScale;
            heldTarget.transform.localScale = heldTarget.transform.localScale * sizeChangeVal;
            //Debug.Log(heldTargetNormalScale + ", " + heldTarget.transform.localScale);

            gameManager.AudioPlayGrab();
        }
        
    }

    private void Throw ()
    {
        if (heldTarget != null)
        {
            heldTarget.transform.parent = null;
            heldTarget.transform.localScale = heldTargetNormalScale;
            grabRb.isKinematic = false;
            grabRb.useGravity = true;

            heldCollider = heldTarget.GetComponent<Collider>();
            if (heldCollider != null)
            {
                heldCollider.enabled = true;
            }

            Transform cam = Camera.main.transform;
            Vector3 dropPos = cam.position + cam.forward * 1.2f;
            dropPos.y = transform.position.y + 2f;
            heldTarget.transform.position = dropPos;


            

            StartCoroutine(Audio_CanDropCoroutine());

            heldTarget = null;
        }
    }

    private void MoveHand()
    {
        if (heldTarget == null)
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed == false) return;

        
        if (grabTarget != null)
        {
            if (heldTarget != null)
            {
                Throw(); 
            }
            Grab(); 
            return;
        }

        
        if (heldTarget != null)
        {
            Throw();
        }
    }



}
