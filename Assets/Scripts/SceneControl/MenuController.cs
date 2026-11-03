using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameManager gm;


    public void StartButton()
    {
        StartCoroutine(gm.BlackFadeIn());
        SceneManager.LoadScene("");
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
    
    public void Button()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
