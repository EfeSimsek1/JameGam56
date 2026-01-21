using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionPanel;

    

    void Start()
    {
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void OnClickstart()
    {
        SceneManager.LoadScene("Game");
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
