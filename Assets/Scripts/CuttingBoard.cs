using UnityEngine;

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
    }
}
