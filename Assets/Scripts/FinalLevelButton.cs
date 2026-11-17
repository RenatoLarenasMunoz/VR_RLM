using UnityEngine;

public class FinalLevelButton : MonoBehaviour, IInteractable
{
    public GameManager gm;

    public void Interact()
    {
        StartCoroutine(gm.BlackFadeIn());
        gm.LastLevel();
    }
}
