using UnityEngine;

public class Bell : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    [SerializeField] Plate plate;

    private void Start()
    {
        GetComponent<Outline>().enabled = false;
>>>>>>> main
    }

    public void Ring()
    {
<<<<<<< HEAD
        if (plate != null)
            plate.EatDinner();
=======
        plate.EatDinner();
>>>>>>> main
    }
}
