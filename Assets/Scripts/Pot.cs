using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private List<Ingredient> ingredients;
    [SerializeField] Recipe recipe;
    [SerializeField] GameObject ruinedDish;
    [SerializeField] Vector3 dishSpawnPos;

    private void Start()
    {
        ingredients = new List<Ingredient>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            ingredients.Add(ingredient);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            ingredients.Remove(ingredient);
        }
    }

    public void CookMeal()
    {
        if(ingredients.Count > 0 && HaveSameItems(recipe.recipe_ingredients, ingredients.Select(x => x.ingredientName).ToList()))
        {
            Debug.Log("Ingredients Matched!");
            Instantiate(recipe.Dish, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ruinedDish, transform.position, Quaternion.identity);
        }

        foreach(Ingredient ingredient in ingredients)
        {
            Destroy(ingredient.gameObject);
        }
    }

    bool HaveSameItems<T>(List<T> a, List<T> b)
    {
        if (a.Count != b.Count)
            return false;

        var counts = new Dictionary<T, int>();

        foreach (var item in a)
        {
            counts[item] = counts.TryGetValue(item, out int c) ? c + 1 : 1;
        }

        foreach (var item in b)
        {
            if (!counts.TryGetValue(item, out int c))
                return false;

            if (--counts[item] == 0)
                counts.Remove(item);
        }

        return counts.Count == 0;
    }
}

[System.Serializable]
public struct Recipe
{
    public List<string> recipe_ingredients;
    public GameObject Dish;
}
