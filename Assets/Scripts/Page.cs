using TMPro;
using UnityEngine;
using System.Collections;

public class Page : MonoBehaviour
{
    public TextMeshProUGUI frontText;
    public TextMeshProUGUI backText;

    public string frontContent;
    public string backContent;

    public float turnSpeed = 2f;
    private bool isTurning = false;

    private void Start()
    {
        frontText.text = frontContent;
        backText.text = backContent;
    }

    public void TurnPage()
    {
        if (isTurning) return;
        StartCoroutine(TurnCoroutine());
    }

    IEnumerator TurnCoroutine()
    {
        isTurning = true;

        Quaternion start = transform.rotation;
        Quaternion target = start * Quaternion.Euler(0, 180, 0);

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * turnSpeed;
            transform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        transform.rotation = target;
        isTurning = false;
    }
}
