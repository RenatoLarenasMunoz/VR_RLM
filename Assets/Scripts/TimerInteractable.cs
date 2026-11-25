using UnityEngine;
using System.Collections;

public class TimerInteractable : MonoBehaviour, IInteractable
{
    public GameObject[] connectedObject;
    public GameManager gm;
    bool isActive = false;
    public float timeLimit;
    [SerializeField] float timer;
    public AudioSource source;
    public AudioClip[] clip;

    private void Start()
    {
        timer = timeLimit;
    }

    public void Interact()
    {
        source.PlayOneShot(clip[0]);
        if (isActive) return;
        print("Button pressed! You have " + timeLimit + " seconds.");
        isActive = true;

        foreach (GameObject obj in connectedObject)
        {
            obj.SetActive(!obj.activeSelf);
        }

        StartCoroutine(TimeActivation());
    }
    IEnumerator TimeActivation()
    {
        source.PlayOneShot(clip[1]);
        while (isActive)
        {
            timer -= Time.deltaTime;

            if (timer - 3 <= 0)
            {
                source.Stop();
                source.PlayOneShot(clip[2]);
            }

            if (timer <= 0)
            {
                isActive = false;
                timer = timeLimit;
                foreach (GameObject obj in connectedObject)
                {
                    obj.SetActive(!obj.activeSelf);
                }
            }

            yield return null;
        }
    }
}
