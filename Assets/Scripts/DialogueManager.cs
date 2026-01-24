using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public int dialogueIndex;
    public bool inDialogue;

    [SerializeField] List<string> startingDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateDialogue(List<string> dialogue_lines)
    {
        StartCoroutine(DialogueString(dialogue_lines));
    }

    public void InitiateStartingDialogue()
    {
        InitiateDialogue(startingDialogue);
    }

    IEnumerator DialogueString(List<string> dialogue_lines)
    {
        inDialogue = true;
        dialogueIndex = 0;
        FPController fpController = FindAnyObjectByType<FPController>();
        if (fpController != null) fpController.PausePlayer();

        TMP_Text dialogueBox = GameObject.Find("Dialogue Box").GetComponent<TMP_Text>();

        while (dialogueIndex < dialogue_lines.Count)
        {
            dialogueBox.text = dialogue_lines[dialogueIndex];
            yield return null;
        }

        dialogueBox.text = "";
        fpController.ResumePlayer();
        inDialogue = false;
    }
}
