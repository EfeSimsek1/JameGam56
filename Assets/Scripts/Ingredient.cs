using UnityEngine;

public class Ingredient : MonoBehaviour
{

    private Outline outline;


    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
