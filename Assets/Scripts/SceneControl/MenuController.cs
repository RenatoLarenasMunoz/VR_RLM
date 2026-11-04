using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameManager gm;


    public void StartButton()
    {
        StartCoroutine(gm.BlackFadeIn());
        SceneManager.LoadScene("Level1");
    }

    public void TutorialButton()
    {
        StartCoroutine(gm.BlackFadeIn());
        SceneManager.LoadScene("LevelTutorial");
    }

    public void RestartButton()
    {
        StartCoroutine(gm.BlackFadeIn());
        gm.ResetLevel();
    }
    
    public void ReturnMenuButton()
    {
        StartCoroutine(gm.BlackFadeIn());
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OptionsButton()
    {

    }
    
    public void Button()
    {

    }

    public void ExitButton()
    {
        StartCoroutine(gm.BlackFadeIn());
        Application.Quit();
    }
}
