using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{

    GameManager gameManager;
    
    //Ray from Player camera
    
    RaycastHit hit;

    GameObject Grabtarget;
    GameObject Heldtarget;
    Rigidbody Grab_rb;
    Collider Held_collider;

    [SerializeField]
    public Transform HandTransform;

    [SerializeField]
    private float HandFollowSpeed = 1f;

    private Vector3 HeldTargetNormalScale;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        CheckTarget();
        //MoveHand();
    }


    private Outline previousOutline;  

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

                Grabtarget = newTarget;
                return;
            }
        }

        
        if (previousOutline != null)
            previousOutline.enabled = false;

        previousOutline = null;
        Grabtarget = null;
    }


    private void Grab()
    {
        
        if (Grabtarget != null && Heldtarget == null)
        {
            Heldtarget = Grabtarget;

            Grab_rb = Heldtarget.GetComponent<Rigidbody>();
            Grab_rb.isKinematic = true;
            Grab_rb.useGravity = false;

            Held_collider = Heldtarget.GetComponent<Collider>();
            if (Held_collider != null)
                Held_collider.enabled = false;

            

            Heldtarget.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            Heldtarget.transform.SetParent(HandTransform);
            Heldtarget.transform.localPosition = Vector3.zero;
            HeldTargetNormalScale = Heldtarget.transform.localScale;
            ///Heldtarget.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            gameManager.AudioPlayGrab();
        }
        
    }

    private void Throw ()
    {
        if (Heldtarget != null)
        {
            Heldtarget.transform.parent = null;
            Heldtarget.transform.localScale = HeldTargetNormalScale;
            Grab_rb.isKinematic = false;
            Grab_rb.useGravity = true;

            Held_collider = Heldtarget.GetComponent<Collider>();
            if (Held_collider != null)
            {
                Held_collider.enabled = true;
            }

            Transform cam = Camera.main.transform;
            Vector3 dropPos = cam.position + cam.forward * 1.2f;
            dropPos.y = transform.position.y + 2f;
            Heldtarget.transform.position = dropPos;


            

            StartCoroutine(Audio_CanDropCoroutine());

            Heldtarget = null;
        }
    }

    private void MoveHand()
    {
        if (Heldtarget == null) return;

        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f);
        float mouseY = (Input.mousePosition.y / Screen.height - 0.5f);

        Vector3 targetLocalPos = new Vector3
            (
                mouseX + 1.5f,   
                mouseY - 0.5f,   
            2f               
            );

        HandTransform.localPosition = Vector3.Lerp(
            HandTransform.localPosition,
            targetLocalPos,
            Time.deltaTime * HandFollowSpeed
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

        
        if (Grabtarget != null)
        {
            if (Heldtarget != null)
            {
                Throw(); 
            }
            Grab(); 
            return;
        }

        
        if (Heldtarget != null)
        {
            Throw();
        }
    }



}
