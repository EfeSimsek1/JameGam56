<<<<<<< HEAD
﻿using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [Header("Ray Settings")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;

    [Header("Hold Settings")]
    [SerializeField] private Transform holdPoint;

    [Header("UI")]
    [SerializeField] private GameObject emptyHandUI;

    private Ingredient heldIngredient;

    private Pot lookingPot;
    private CuttingBoard lookingBoard;
    private Bell lookingBell;
    private Ingredient lookingIngredient;

    void Update()
    {
        HandleRay();
        HandleUI();
        HandleInteract();
    }

    // 레이캐스트로 바라보는 대상 감지 및 하이라이트
    void HandleRay()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // 이전 하이라이트 초기화
        if (lookingIngredient != null) { lookingIngredient.SetHighlight(false); lookingIngredient = null; }
        if (lookingPot != null) { lookingPot.Highlight(false); lookingPot = null; }
        if (lookingBoard != null) { lookingBoard.Highlight(false); lookingBoard = null; }
        if (lookingBell != null) { lookingBell.Highlight(false); lookingBell = null; }

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            Ingredient ing = hit.collider.GetComponent<Ingredient>();
            if (ing != null) { lookingIngredient = ing; ing.SetHighlight(true); }

            Pot pot = hit.collider.GetComponent<Pot>();
            if (pot != null) { lookingPot = pot; pot.Highlight(true); }

            CuttingBoard board = hit.collider.GetComponent<CuttingBoard>();
            if (board != null) { lookingBoard = board; board.Highlight(true); }

            Bell bell = hit.collider.GetComponent<Bell>();
            if (bell != null) { lookingBell = bell; bell.Highlight(true); }
        }
    }

    // E키 입력 처리
    void HandleInteract()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        // 🔹 냄비에 넣기
        if (heldIngredient != null && lookingPot != null)
        {
            lookingPot.PutIngredient(heldIngredient);
            heldIngredient = null;
            return;
        }

        // 🔹 도마에 놓고 썰기
        if (heldIngredient != null && lookingBoard != null && heldIngredient.canBeCut)
        {
            lookingBoard.PutIngredient(heldIngredient);
            heldIngredient = null;
            return;
        }

        // 🔹 벨 울리기
        if (lookingBell != null)
        {
            lookingBell.Ring();
            return;
        }

        // 🔹 재료 줍기
        if (heldIngredient == null && lookingIngredient != null)
        {
            PickUp(lookingIngredient);
        }
    }

    // 재료 줍기
    void PickUp(Ingredient ingredient)
    {
        heldIngredient = ingredient;

        Rigidbody rb = ingredient.GetComponent<Rigidbody>();
        Collider col = ingredient.GetComponent<Collider>();

        if (rb != null) { rb.isKinematic = true; rb.useGravity = false; }
        if (col != null) col.enabled = false;

        ingredient.transform.SetParent(holdPoint);
        ingredient.transform.localPosition = Vector3.zero;
        ingredient.transform.localRotation = Quaternion.identity;
    }

    // UI 처리
    void HandleUI()
    {
        if (emptyHandUI != null)
            emptyHandUI.SetActive(heldIngredient == null);
    }

    // 외부에서 가지고 있는 재료 가져오기 (필요 시)
    public Ingredient GetHeldIngredient()
    {
        return heldIngredient;
    }

    // 외부에서 재료 해제 (필요 시)
    public void ClearHeldIngredient()
    {
        heldIngredient = null;
=======
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

    [SerializeField] LayerMask interactable;

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

        if (taskTarget != null && heldObject != null && heldObject.GetComponent<Ingredient>() != null && heldObject.GetComponent<Ingredient>().canBeCut)
        {
            CuttingBoard board = taskTarget.GetComponent<CuttingBoard>();

            if (board != null)
            {
                board.CutIngredient(heldObject);
                heldObject = null;
                return;
            }
        }

        if(taskTarget != null && taskTarget.GetComponent<Bell>() != null)
        {
            taskTarget.GetComponent<Bell>().Ring();
        }

        if (heldObject != null)
        {
            Throw();
        }
>>>>>>> main
    }
}
