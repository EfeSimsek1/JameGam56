using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    
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

    private void Start()
    {
        
    }
    void Update()
    {
        CheckTarget();
        Grab();
        Throw();
        MoveHand();
        
    }
    
    
    private void CheckTarget ()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                Grabtarget = hit.collider.gameObject;
                return;
            }
        }

        Grabtarget = null;
                
    }

    private void Grab()
    {
        if (Grabtarget != null && Heldtarget == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Heldtarget = Grabtarget;

                Grab_rb = Heldtarget.GetComponent<Rigidbody>();
                Grab_rb.isKinematic = true;
                Grab_rb.useGravity = false;

                Held_collider = Heldtarget.GetComponent<Collider>();
                if (Held_collider != null)
                    Held_collider.enabled = false;

                Heldtarget.transform.SetParent(HandTransform);
                Heldtarget.transform.localPosition = Vector3.zero;
                Heldtarget.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    private void Throw ()
    {
        
    }

    private void MoveHand()
    {
        if (Heldtarget == null) return;

        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f);
        float mouseY = (Input.mousePosition.y / Screen.height - 0.5f);

        Vector3 targetLocalPos = new Vector3(
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


}
