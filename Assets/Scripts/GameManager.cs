using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int piecesCollected;
    public Canvas gameCanvas;
    public GameObject[] finishLine;
    public GameObject[] pieceIndicator;
    public GameObject textInstruction;
    public GameObject textEndLevel;
    public AudioSource source;
    public AudioClip[] clip;
    //public AudioClip[] music;

    public ResetScene resetScene;


    public bool exitOn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitOn = false;

        foreach (GameObject icon in pieceIndicator)
        {
            icon.SetActive(false);
        }

        StartCoroutine(resetScene.BlackFadeOut());
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

        //PlaySFX(2); // sonido no disponible
    }

    public void GameOver()
    {
        //PlaySFX(3); // sonido no disponible
    }
}
