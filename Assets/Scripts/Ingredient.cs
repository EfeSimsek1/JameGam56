using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;
<<<<<<< HEAD
    public string ingredientType;

    [SerializeField] private GameObject cutPrefab; // 썰린 조각 프리팹
    public Vector3 cutAnimLocalPos;
    public Vector3 cutAnimLocalRotation;
    [SerializeField] private float splitOffset = 0.1f;

=======
>>>>>>> main
    private Outline outline;
    [SerializeField] private GameObject cutIngredient;
    public Vector3 cutAnimLocalPos;
    public Vector3 cutAnimLocalRotation;
    public bool canBeCut;

<<<<<<< HEAD
=======

    [Tooltip("0.1 ~ 1")]  public float sizeChangeValue = 1f;

>>>>>>> main
    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    public bool canBeCut => cutPrefab != null;

    public void SetHighlight(bool on)
    {
<<<<<<< HEAD
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
=======
        canBeCut = cutIngredient != null;
    }

    public void Cut()
    {
        if (!cutIngredient) return;

        GameObject cutIngredientParent = Instantiate(cutIngredient, gameObject.transform.position, gameObject.transform.rotation);
        Transform child1 = cutIngredientParent.transform.GetChild(0);
        child1.parent = null;
        child1.GetComponent<Ingredient>().ingredientName = ingredientName + " slice";

        Transform child2 = cutIngredientParent.transform.GetChild(0);
        child2.parent = null;
        child2.GetComponent<Ingredient>().ingredientName = ingredientName + " slice";
        Destroy(gameObject);
>>>>>>> main
    }
}
