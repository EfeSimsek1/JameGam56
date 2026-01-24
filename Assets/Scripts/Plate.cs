using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private List<Ingredient> ingredients;
    [SerializeField] private List<IngredientCombination> winning_combos;
    public GameObject virtual_cam;
    [SerializeField] private TMP_Text playerDialogue;

    private void Awake()
    {
        ingredients = new List<Ingredient>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null) ingredients.Add(ingredient);
    }

    private void OnTriggerExit(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null) ingredients.Remove(ingredient);
    }

    public void EatDinner()
    {
        StartCoroutine(RemoveIngredients());
    }

    public IEnumerator RemoveIngredients()
    {
        int numOnPlate = ingredients.Count;
        virtual_cam.SetActive(true);
        yield return new WaitForSeconds(3);

        while (ingredients.Count > 0)
        {
            Ingredient item = ingredients[0];
            foreach (IngredientCombination combo in winning_combos)
            {
                if (combo.ingredients.Contains(item.ingredientName))
                    combo.ingredients.Remove(item.ingredientName);
            }

            ingredients.RemoveAt(0);
            Destroy(item.gameObject);
            yield return new WaitForSeconds(0.5f);
        }

        bool won_game = false;
        foreach (IngredientCombination combo in winning_combos)
        {
            if (combo.ingredients.Count == 0)
            {
                playerDialogue.text = "An Apple, Sweet! Level Up! XD";
                won_game = true;
            }
        }

        if (!won_game && numOnPlate == 0)
            playerDialogue.text = "I couldn't even bother making dinner... Whatever, I'm going to bed.";
        else if (!won_game && numOnPlate == 1)
            playerDialogue.text = "These aren't what I wanted... Whatever, I'm going to bed.";
        else if (!won_game)
            playerDialogue.text = "This isn't what I wanted... Whatever, I'm going to bed.";
    }

    [System.Serializable]
    public struct IngredientCombination
    {
        public List<string> ingredients;
    }
}
