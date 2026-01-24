using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] private Plate plate;
    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    public void Highlight(bool on)
    {
        if (outline != null) outline.enabled = on;
    }

    public void Ring()
    {
        if (plate != null)
            plate.EatDinner();
    }
}
