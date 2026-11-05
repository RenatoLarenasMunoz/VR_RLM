using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Game Elements")]
    public int piecesCollected;
    public Canvas gameCanvas;
    public GameObject[] finishLine;
    public GameObject[] pieceIndicator;
    public GameObject textInstruction;
    public GameObject textEndLevel;
    public GameObject restartButton;
    public bool exitOn;
    
    [Header("Sound Settings")]
    public AudioSource source;
    public AudioClip[] clip;
    //public AudioClip[] music;

    [Header("Scene Manager Settings")]
    public ResetScene resetScene;
    public Image imagenFade;
    public float speedFade;
    public int sceneIntex;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartButton.SetActive(false);
        exitOn = false;

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
            piecesCollected = 3;
            ActivateExit();
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
        textInstruction.SetActive(!textInstruction.activeSelf);
        textEndLevel.SetActive(!textEndLevel.activeSelf);
        restartButton.SetActive(true);

        PlaySFX(2);
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
