using UnityEngine;

public class FridgeDoor : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        if (!isOpen)
        {
            anim.Play("FridgeOpen", 0, 0f);
            isOpen = true;
        }
        else
        {
            anim.Play("FridgeClose", 0, 0f);
            isOpen = false;
        }
    }
}
