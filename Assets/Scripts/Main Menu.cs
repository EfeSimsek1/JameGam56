using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionPanel;

    private Fade fade;
    

    void Start()
    {
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
        fade = FindFirstObjectByType<Fade>();
    }

    
    void Update()
    {
        
    }

    public void OnClickstart()
    {
        fade.FadeIn();
        
        
    }

    public void OnClickCredits ()
    {

    }
    
    public void OnClickOptions ()
    {
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }
     
    public void OnClickQuit ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif  
        
    }

    public void OnClickQuitOptions ()
    {
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }



}
