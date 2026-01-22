using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Book : MonoBehaviour
{
    [SerializeField] private float bookSpeed = 1f;
    [SerializeField] private List<Transform> pages;

    private int index = 0;          
    private bool isTurning = false; 

    
    public void OnClickNextPage()
    {
        if (isTurning || index >= pages.Count) return;
        StartCoroutine(PageCoroutine(true));
    }

    
    public void OnClickBackPage()
    {
        if (isTurning || index <= 0) return;
        StartCoroutine(PageCoroutine(false));
    }

    private IEnumerator PageCoroutine(bool forward)
    {
        isTurning = true;

        
        Transform page = forward ? pages[index] : pages[index - 1];

        Quaternion startRotation = page.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, forward ? 180f : 0f, 0f);

        float t = 0f;
        float duration = 1f / bookSpeed;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            page.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        page.localRotation = targetRotation;

        index += forward ? 1 : -1;
        isTurning = false;
    }
}
