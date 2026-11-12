using UnityEngine;

public class NextLevelButton : MonoBehaviour, IInteractable
{
    public GameManager gm;

    public void Interact()
    {
        StartCoroutine(gm.BlackFadeIn());
        gm.NextLevel();
    }
}