using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private PlayerGrab playergrab;
    private GameManager gamemanager;
    public Pottext pottext;
    public Dish dish;
    
    public List<string> ingredients = new List<string>();
    private Outline outline;

    private void Start()
    {
        dish.gameObject.SetActive(false);

        playergrab = FindAnyObjectByType<PlayerGrab>();
        gamemanager = FindAnyObjectByType<GameManager>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void PutIngredient(GameObject ingredientObject)
    {
        if (ingredientObject == null) return;

        Ingredient ingredientComp = ingredientObject.GetComponent<Ingredient>();
        if (ingredientComp == null) return;

        string ingredientName = ingredientComp.ingredientName;
        ingredients.Add(ingredientName); 
        Debug.Log(ingredientName);

        
        if (pottext != null)
            pottext.IngredientsPutText();

        Destroy(ingredientObject);
    }

    public void MakeDish (string dishType)
    {
        dish.dishType = dishType;
        dish.gameObject.SetActive(true);
    }



}
