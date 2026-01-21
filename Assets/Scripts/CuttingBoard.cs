using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    private Outline outline;
    [SerializeField] private Animation cuttingAnim;
    private Ingredient storedIngredient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CutIngredient(GameObject ingredientObj)
    {
        if (ingredientObj != null)
        {
            Ingredient ingredient = ingredientObj.GetComponent<Ingredient>();

            ingredientObj.transform.SetParent(gameObject.transform);
            ingredientObj.transform.localPosition = ingredient.cutAnimLocalPos;
            ingredientObj.transform.localRotation = Quaternion.Euler(ingredient.cutAnimLocalRotation);

            storedIngredient = ingredient;
        }

        cuttingAnim.Play();
    }

    public void CutStoredIngredient()
    {
        if (storedIngredient != null)
        {
            storedIngredient.Cut();
        }
    }
}
