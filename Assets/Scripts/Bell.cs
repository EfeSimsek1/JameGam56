using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] Plate plate;

    private void Start()
    {
        GetComponent<Outline>().enabled = false;
    }

    public void Ring()
    {
        plate.EatDinner();
    }
}
