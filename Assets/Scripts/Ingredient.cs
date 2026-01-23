using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;
    public string ingredientType;

    [SerializeField] private GameObject cutPrefab; // 썰린 조각 프리팹
    public Vector3 cutAnimLocalPos;
    public Vector3 cutAnimLocalRotation;
    [SerializeField] private float splitOffset = 0.1f;

    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    public bool canBeCut => cutPrefab != null;

    public void SetHighlight(bool on)
    {
        if (outline != null) outline.enabled = on;
    }

    // Cut 애니메이션 의존 X, 2조각 생성
    public void Cut()
    {
        if (cutPrefab == null) return;

        GameObject left = Instantiate(cutPrefab, transform.position + Vector3.left * splitOffset, transform.rotation);
        Ingredient leftIng = left.GetComponent<Ingredient>();
        if (leftIng != null)
        {
            leftIng.ingredientName = ingredientName + " slice";
            leftIng.ingredientType = ingredientType;
        }

        GameObject right = Instantiate(cutPrefab, transform.position + Vector3.right * splitOffset, transform.rotation);
        Ingredient rightIng = right.GetComponent<Ingredient>();
        if (rightIng != null)
        {
            rightIng.ingredientName = ingredientName + " slice";
            rightIng.ingredientType = ingredientType;
        }

        Destroy(gameObject); // 원본 삭제
    }
}
