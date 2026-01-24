using TMPro;
using UnityEngine;
using System.Text;
using System.Collections;

public class Pottext : MonoBehaviour
{
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private Pot pot;
    public TextMeshProUGUI potText;
    [SerializeField] private string emptyText = "Put some Ingredient here";

    private bool isShowingText = false; 
    private Coroutine currentCoroutine;

    private void Start()
    {
        potText.text = "";
    }

    public void IngredientsPutTextZero()
    {
        Debug.Log("Zero");
        if (!isShowingText)
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(ZeroCoroutine(waitTime));
        }
    }

    public void IngredientsPutText()
    {
        Debug.Log("Put");
        if (!isShowingText)
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(PutCoroutine(waitTime));
        }
    }

    private IEnumerator ZeroCoroutine(float waitTime)
    {
        isShowingText = true;

        potText.text = emptyText;

        
        yield return new WaitForSeconds(waitTime + 0.9f);

        potText.text = null;
        isShowingText = false;
    }


    private IEnumerator PutCoroutine(float waitTime)
    {
        isShowingText = true; 

        potText.text = "You have put an Ingredient.";

        
        yield return new WaitForSeconds(waitTime + 0.9f);

        potText.text = null;
        isShowingText = false; 
    }

}
