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
    [SerializeField] GameObject restartMenu;
    public GameObject virtual_cam;
    public GameObject death_virtual_cam;
    [SerializeField] List<string> unsatisfiedMealLinesSingular;
    [SerializeField] List<string> unsatisfiedMealLinesPlural;
    [SerializeField] List<string> satisfiedMealLines;
    [SerializeField] List<string> partialMatchLines;
    [SerializeField] List<string> emptyPlateLines;
    [SerializeField] List<string> dyingLines;

    private GameManager gameManager;

    private int strikes;

    private void Awake()
    {
        ingredients = new List<Ingredient>();
        strikes = 3;
        gameManager = FindAnyObjectByType<GameManager>();
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

        /*var req = winning_combos
            .Select(c =>
            {
                var copy = c; // struct copy
                copy.ingredients = new List<string>(c.ingredients); // deep-copy inner list
                return copy;
            }).ToList();*/

        FindAnyObjectByType(typeof(FPController)).GetComponent<FPController>().PausePlayer();
        virtual_cam.SetActive(true);

        yield return new WaitForSeconds(3);

        int items_matched = 0;

        while (ingredients.Count > 0)
        {
            Ingredient item = ingredients[0];
            bool combo_matched = false;

            foreach(IngredientCombination combo in winning_combos)
            {
                if (combo.ingredients.Contains(item.ingredientName))
                {
                    combo.ingredients.Remove(item.ingredientName);
                    items_matched++;
                    combo_matched = true;
                }
            }

            if (!combo_matched) strikes--;

            gameManager.AudioPlayEat();

            ingredients.RemoveAt(0);
            Destroy(item.gameObject);
            yield return new WaitForSeconds(0.5f);
        }

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
        bool obtained_winning_combo = false;

        foreach (IngredientCombination combo in winning_combos)
        {
            if (combo.ingredients.Count == 0)
            {
                obtained_winning_combo = true;
            }
        }

        if (!obtained_winning_combo && numOnPlate == 0) dialogueManager.InitiateDialogue(emptyPlateLines);
        else if (!obtained_winning_combo && numOnPlate == 1) dialogueManager.InitiateDialogue(unsatisfiedMealLinesSingular);
        else if (!obtained_winning_combo && items_matched == 0) dialogueManager.InitiateDialogue(unsatisfiedMealLinesPlural);
        else if (!obtained_winning_combo && items_matched > 0) dialogueManager.InitiateDialogue(partialMatchLines);
        else
        {
            dialogueManager.InitiateDialogue(satisfiedMealLines);
            yield return new WaitUntil(() => !dialogueManager.inDialogue);
            FindAnyObjectByType<TransitionManager>().FadeOutNextLevel(3f);
            FindAnyObjectByType<FPController>().GetComponent<FPController>().PausePlayer();
        }
        
        yield return new WaitUntil(() => !dialogueManager.inDialogue);

        if (strikes <= 0)
        {
            #region Die

            dialogueManager.InitiateDialogue(dyingLines);
            yield return new WaitUntil(() => !dialogueManager.inDialogue);
            FindAnyObjectByType<TransitionManager>().GetComponent<TransitionManager>().FadeOut("Main Menu", 3f);
            FindAnyObjectByType<FPController>().GetComponent<FPController>().PausePlayer();
            death_virtual_cam.SetActive(true);

            #endregion
        }

        virtual_cam.SetActive(false);

        yield return new WaitForSeconds(2f);

        FindAnyObjectByType(typeof(FPController)).GetComponent<FPController>().ResumePlayer();;
    }

    [System.Serializable]
    public struct IngredientCombination
    {
        public List<string> ingredients;
    }

}
