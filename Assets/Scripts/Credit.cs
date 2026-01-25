using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private GameObject credit;
    [SerializeField] private MainMenu mainMenu;

    [SerializeField] private float creditSpeed = 30f;

    [HideInInspector] public bool creditStart = false;
    private void Update()
    {
        if (creditStart == false)
        credit.transform.position = new Vector3(0, -600, 0);

        if (creditStart == true)
        {
            credit.transform.position += new Vector3(0, creditSpeed, 0);
            if (Input.anyKeyDown)
            {
                creditStart = false;
                mainMenu.mainMenuPanel.SetActive(true);
                mainMenu.creditPanel.SetActive(false);
            }
        }
            
    }

    
}
