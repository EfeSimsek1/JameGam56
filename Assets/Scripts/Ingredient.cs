using UnityEngine;

public class Ingredient : MonoBehaviour
{

    private Outline outline;


    [Tooltip("0.1 ~ 1")]  public float SizeChangeValue;

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
