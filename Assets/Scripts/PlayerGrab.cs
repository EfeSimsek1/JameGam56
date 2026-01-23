using UnityEngine;

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
    }
}
