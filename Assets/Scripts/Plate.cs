using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    List<Ingredient> ingredients;
    [SerializeField] private List<IngredientCombination> winning_combos;
    public GameObject virtual_cam;
    [SerializeField] List<string> unsatisfiedMealLinesSingular;
    [SerializeField] List<string> unsatisfiedMealLinesPlural;
    [SerializeField] List<string> satisfiedMealLines;
    [SerializeField] List<string> emptyPlateLines;

    private void Awake()
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

    public void EatDinner()
    {
        StartCoroutine(RemoveIngredients());
    }

    public IEnumerator RemoveIngredients()
    {
        int numOnPlate = ingredients.Count;

        var req = winning_combos
            .Select(c =>
            {
                var copy = c; // struct copy
                copy.ingredients = new List<string>(c.ingredients); // deep-copy inner list
                return copy;
            }).ToList();

        FindAnyObjectByType(typeof(FPController)).GetComponent<FPController>().PausePlayer();
        virtual_cam.SetActive(true);

        yield return new WaitForSeconds(3);

        while (ingredients.Count > 0)
        {
            Ingredient item = ingredients[0];

            foreach(IngredientCombination combo in req)
            {
                if (combo.ingredients.Contains(item.ingredientName)) combo.ingredients.Remove(item.ingredientName);
            }

            ingredients.RemoveAt(0);
            Destroy(item.gameObject);
            yield return new WaitForSeconds(0.5f);
        }

        bool won_game = false;

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        foreach (IngredientCombination combo in req)
        {
            if (combo.ingredients.Count == 0)
            {
                dialogueManager.InitiateDialogue(satisfiedMealLines);
                // Win Game
                won_game = true;
            }
        }

        if (!won_game && numOnPlate == 0) dialogueManager.InitiateDialogue(emptyPlateLines);
        else if (!won_game && numOnPlate == 1) dialogueManager.InitiateDialogue(unsatisfiedMealLinesSingular);
        else if (!won_game) dialogueManager.InitiateDialogue(unsatisfiedMealLinesPlural);

        yield return new WaitUntil(() => !dialogueManager.inDialogue);

        virtual_cam.SetActive(false);

        yield return new WaitForSeconds(2f);

        FindAnyObjectByType(typeof(FPController)).GetComponent<FPController>().ResumePlayer();

        //End Game
        //GameManager.instance.FadeAndLoad("MainMenu", 3);
    }

    [System.Serializable]
    public struct IngredientCombination
    {
        public List<string> ingredients;
    }

}
