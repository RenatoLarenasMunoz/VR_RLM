using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetScene : MonoBehaviour, IInteractable
{
    public Image imagenFade;
    public float speedFade;

    public void Interact()
    {
        StartCoroutine(BlackFadeIn());
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("GameLayoutTest");
    }

    public IEnumerator BlackFadeIn()
    {
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * speedFade;

            float finalAlpha = Mathf.Lerp(0, 1, elapsedTime);

            imagenFade.color = new Color(0, 0, 0, finalAlpha);

            yield return null;
        }

        ResetLevel();
    }

    public IEnumerator BlackFadeOut()
    {
        float elapsedTime = 0;

        yield return new WaitForSeconds(1f);

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * speedFade;

            float finalAlpha = Mathf.Lerp(1, 0, elapsedTime);

            imagenFade.color = new Color(0, 0, 0, finalAlpha);

            yield return null;
        }
    }
}
