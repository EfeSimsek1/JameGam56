<<<<<<< HEAD
﻿using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    [SerializeField] private Outline outline;

    void Awake()
    {
        if (outline != null) outline.enabled = false;
    }

    public void Highlight(bool on)
    {
        if (outline != null) outline.enabled = on;
    }

    public void PutIngredient(Ingredient ingredient)
    {
        if (ingredient == null) return;

        ingredient.transform.SetParent(transform);
        ingredient.transform.localPosition = ingredient.cutAnimLocalPos;
        ingredient.transform.localRotation = Quaternion.Euler(ingredient.cutAnimLocalRotation);

        ingredient.Cut(); // 2조각으로 나누고 원본 삭제
=======
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
>>>>>>> main
    }
}
