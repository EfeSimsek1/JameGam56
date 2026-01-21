using UnityEngine;

public class Ingredient : MonoBehaviour
{

    private Outline outline;
    [SerializeField] private GameObject cutIngredient;


    [Tooltip("0.1 ~ 1")]  public float sizeChangeValue = 1f;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cut()
    {
        if (!cutIngredient) return;

        GameObject cutIngredientParent = Instantiate(cutIngredient, gameObject.transform.position, gameObject.transform.rotation);
        cutIngredientParent.transform.GetChild(0).parent = null;
        cutIngredientParent.transform.GetChild(0).parent = null;
        Destroy(gameObject);
    }
}
