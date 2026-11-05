using UnityEngine;
using System.Collections;

public class TimerInteractable : MonoBehaviour, IInteractable
{
    public GameObject[] connectedObject;
    public GameManager gm;
    bool isActive = false;
    public float timeLimit;
    [SerializeField] float timer;

    private void Start()
    {
        timer = timeLimit;
    }

    public void Interact()
    {
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
        while (isActive)
        {
            timer -= Time.deltaTime;

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
