using UnityEngine;

public class Ingredient : MonoBehaviour
{

    private Outline outline;
    [SerializeField] private GameObject cutIngredient;
    public Vector3 cutAnimLocalPos;
    public Vector3 cutAnimLocalRotation;
    public bool canBeCut;


    [Tooltip("0.1 ~ 1")]  public float sizeChangeValue = 1f;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        canBeCut = cutIngredient != null;
    }

    public void Cut()
    {
        if (!cutIngredient) return;

        GameObject cutIngredientParent = Instantiate(cutIngredient, gameObject.transform.position, gameObject.transform.rotation);
        Transform child1 = cutIngredientParent.transform.GetChild(0);
        child1.parent = null;

        Transform child2 = cutIngredientParent.transform.GetChild(0);
        child2.parent = null;
        Destroy(gameObject);
    }
}
