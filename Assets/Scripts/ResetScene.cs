using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetScene : MonoBehaviour, IInteractable
{
    public GameManager gm;

    public void Interact()
    {
        StartCoroutine(gm.BlackFadeIn());
    }
}
