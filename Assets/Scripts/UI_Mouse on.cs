using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Mouseon : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public CanvasGroup cg;

    private float normalAlpha;

    void Awake()
    {
        if (cg == null)
            cg = GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        normalAlpha = cg.alpha;
        cg.alpha = 0.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cg.alpha = normalAlpha;
    }
}
