<<<<<<< HEAD
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
=======
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
>>>>>>> main

    private void Awake()
    {
        ingredients = new List<Ingredient>();
    }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null) ingredients.Add(ingredient);
=======
        Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            ingredients.Add(ingredient);
        }
>>>>>>> main
    }

    private void OnTriggerExit(Collider other)
    {
<<<<<<< HEAD
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null) ingredients.Remove(ingredient);
=======
        Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            ingredients.Remove(ingredient);
        }
>>>>>>> main
    }

    public void EatDinner()
    {
        StartCoroutine(RemoveIngredients());
    }

    public IEnumerator RemoveIngredients()
    {
        int numOnPlate = ingredients.Count;
<<<<<<< HEAD
        virtual_cam.SetActive(true);
=======

        var req = winning_combos
            .Select(c =>
            {
                var copy = c; // struct copy
                copy.ingredients = new List<string>(c.ingredients); // deep-copy inner list
                return copy;
            }).ToList();

        FindAnyObjectByType(typeof(FPController)).GetComponent<FPController>().PausePlayer();
        virtual_cam.SetActive(true);

>>>>>>> main
        yield return new WaitForSeconds(3);

        while (ingredients.Count > 0)
        {
            Ingredient item = ingredients[0];
<<<<<<< HEAD
            foreach (IngredientCombination combo in winning_combos)
            {
                if (combo.ingredients.Contains(item.ingredientName))
                    combo.ingredients.Remove(item.ingredientName);
=======

            foreach(IngredientCombination combo in req)
            {
                if (combo.ingredients.Contains(item.ingredientName)) combo.ingredients.Remove(item.ingredientName);
>>>>>>> main
            }

            ingredients.RemoveAt(0);
            Destroy(item.gameObject);
            yield return new WaitForSeconds(0.5f);
        }

        bool won_game = false;
<<<<<<< HEAD
        foreach (IngredientCombination combo in winning_combos)
        {
            if (combo.ingredients.Count == 0)
            {
                playerDialogue.text = "An Apple, Sweet! Level Up! XD";
=======

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();

        foreach (IngredientCombination combo in req)
        {
            if (combo.ingredients.Count == 0)
            {
                dialogueManager.InitiateDialogue(satisfiedMealLines);
                // Win Game
>>>>>>> main
                won_game = true;
            }
        }

<<<<<<< HEAD
        if (!won_game && numOnPlate == 0)
            playerDialogue.text = "I couldn't even bother making dinner... Whatever, I'm going to bed.";
        else if (!won_game && numOnPlate == 1)
            playerDialogue.text = "These aren't what I wanted... Whatever, I'm going to bed.";
        else if (!won_game)
            playerDialogue.text = "This isn't what I wanted... Whatever, I'm going to bed.";
=======
        if (!won_game && numOnPlate == 0) dialogueManager.InitiateDialogue(emptyPlateLines);
        else if (!won_game && numOnPlate == 1) dialogueManager.InitiateDialogue(unsatisfiedMealLinesSingular);
        else if (!won_game) dialogueManager.InitiateDialogue(unsatisfiedMealLinesPlural);

        yield return new WaitUntil(() => !dialogueManager.inDialogue);

        virtual_cam.SetActive(false);

        yield return new WaitForSeconds(2f);

        FindAnyObjectByType(typeof(FPController)).GetComponent<FPController>().ResumePlayer();

        //End Game
        //GameManager.instance.FadeAndLoad("MainMenu", 3);
>>>>>>> main
    }

    [System.Serializable]
    public struct IngredientCombination
    {
        public List<string> ingredients;
    }
<<<<<<< HEAD
=======

>>>>>>> main
}
