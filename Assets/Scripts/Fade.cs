using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    
    public CanvasGroup cg;
    void Start()
    {
        if (cg == null)
        {
            cg = GetComponent<CanvasGroup>();
        }

        cg.alpha = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn ()
    {
        StartCoroutine(FadeInCoroutine());
    }

    
    IEnumerator FadeInCoroutine ()
    {
        
        for (float i = cg.alpha; i <= 1; i = i  +0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            cg.alpha = i;
            
            
        }
        SceneManager.LoadScene("Game");
        
}

}
