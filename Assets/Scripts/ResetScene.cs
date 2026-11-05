using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour, IInteractable
{
    public GameManager gm;

    public void Interact()
    {
        StartCoroutine(gm.BlackFadeIn());
        gm.ResetLevel();
    }
}