using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Mouseon : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public CanvasGroup cg;

    private float normalAlpha;

    private MainMenu menu;

    void Awake()
    {
        if (cg == null)
            cg = GetComponent<CanvasGroup>();

        menu = FindAnyObjectByType<MainMenu>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (menu.is_Started == true) return;
        
        normalAlpha = cg.alpha;
        cg.alpha = 0.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (menu.is_Started == true) return;

        cg.alpha = normalAlpha;
    }

    private void OnEnable()
    {
        if (cg == null)
        {
            cg = GetComponent<CanvasGroup>();
        }
        cg.alpha = 1.0f;
    }
}
