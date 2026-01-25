using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionPanel;

    private TransitionManager fade;

    public bool is_Started = false;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
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
