using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotButton : MonoBehaviour
{
    private Outline outline;
    public Pot pot;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
}