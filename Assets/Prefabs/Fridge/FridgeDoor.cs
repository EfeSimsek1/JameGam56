using UnityEngine;

public class FridgeDoor : MonoBehaviour
{
    [SerializeField] private Animator topDoorAnimator;
    [SerializeField] private Animator bottomDoorAnimator;

    private bool isOpen = false;

    private void Start()
    {
        GetComponent<Outline>().enabled = false;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;

        if (topDoorAnimator != null)
            topDoorAnimator.Play(isOpen ? "FridgeOpen" : "FridgeClose", 0, 0f);

        if (bottomDoorAnimator != null)
            bottomDoorAnimator.Play(isOpen ? "BottomFridgeOpen" : "BottomFridgeClose", 0, 0f);
    }
}
