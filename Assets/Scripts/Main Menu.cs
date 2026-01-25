using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionPanel;
    public GameObject creditPanel;
    private TransitionManager fade;

    public Credit credit;
    public bool is_Started = false;

    void Start()
    {
        if(creditPanel)creditPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        if(optionPanel)optionPanel.SetActive(false);
        fade = FindFirstObjectByType<TransitionManager>();
    }

    
    void Update()
    {
        
    }

    public void OnClickstart()
    {
        if (is_Started == true) return;
        
        fade.FadeOut("Level1", 2f);
    }

    public void OnClickCredits ()
    {
        if (is_Started == true) return;
        credit.creditStart = true;
        creditPanel.SetActive (true);
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive (false);

        
    }
    
    public void OnClickOptions ()
    {
        if (is_Started == true) return;
        
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }
     
    public void OnClickQuit ()
    {
        if (is_Started == true) return;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif  
        
    }

    public void OnClickQuitOptions ()
    {
        if (is_Started == true) return;

        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }



}
