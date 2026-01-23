using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] private Outline outline;

    public void Highlight(bool on)
    {
        if (outline != null) outline.enabled = on;
    }

    public void PutIngredient(Ingredient ingredient)
    {
        if (ingredient == null) return;

        Debug.Log("냄비에 넣음: " + ingredient.ingredientName);
        Destroy(ingredient.gameObject); // 재료 삭제
    }
}
