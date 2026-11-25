using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Elements")]
    public int piecesCollected;
    public Canvas gameCanvas;
    public GameObject[] finishLine;
    public GameObject[] pieceIndicator;
    public TextMeshProUGUI[] textLevel;
    public GameObject textInstruction;
    int textIndex;
    public GameObject textEndLevel;
    public GameObject restartButton;
    public bool exitOn;
    
    [Header("Sound Settings")]
    public AudioSource source;
    public AudioClip[] clip;
    public AudioClip buttonClip;
    //public AudioClip[] music;

    [Header("Scene Manager Settings")]
    public Image imagenFade;
    public float speedFade;
    public int sceneIntex;
    bool levelComplete;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(restartButton != null)
        {
            restartButton.SetActive(false);
        }
        exitOn = false;
        levelComplete = false;

        foreach (GameObject icon in pieceIndicator)
        {
            icon.SetActive(false);
        }

        StartCoroutine(BlackFadeOut());
    }

    public void CollectPiece()
    {
        piecesCollected++;

        pieceIndicator[piecesCollected - 1].SetActive(!pieceIndicator[piecesCollected - 1].activeSelf);

        if (piecesCollected >= 3)
        {
            textIndex = 0;
            piecesCollected = 3;
            ActivateExit();
            StartCoroutine(TextFadeOut());
        }

        PlaySFX(0);
    }

    void ActivateExit()
    {
        Debug.Log("Piezas conseguidas, regreza al inicio");

        exitOn = true;
        textInstruction.SetActive(!textInstruction.activeSelf);

        foreach (GameObject item in finishLine)
        {
            item.SetActive(!item.activeSelf);
        }

        PlaySFX(1);
    }

    public void PlaySFX(int index)
    {
        source.PlayOneShot(clip[index]);
    }

    void PlayMusic()
    {

    }

    public void EndLevel()
    {
        if (!levelComplete)
        {
            textIndex = 1;
            textInstruction.SetActive(!textInstruction.activeSelf);
            textEndLevel.SetActive(!textEndLevel.activeSelf);
            restartButton.SetActive(true);
            StartCoroutine(TextFadeOut());

            PlaySFX(2);
            levelComplete = true;
        }
    }

    public void GameOver()
    {
        PlaySFX(3);

        StartCoroutine(BlackFadeIn());
        ResetLevel();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(sceneIntex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(sceneIntex + 1);
    }
    public void LastLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator BlackFadeIn()
    {
        float elapsedTime = 0;
        source.PlayOneShot(buttonClip);

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * speedFade;

            float finalAlpha = Mathf.Lerp(0, 1, elapsedTime);

            imagenFade.color = new Color(0, 0, 0, finalAlpha);

            yield return null;
        }
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

    public IEnumerator TextFadeOut()
    {
        float elapsedTime = 0;

        yield return new WaitForSeconds(1f);

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * speedFade;

            float finalAlpha = Mathf.Lerp(1, 0, elapsedTime);

            textLevel[textIndex].alpha = finalAlpha;

            yield return null;
        }
    }
}
