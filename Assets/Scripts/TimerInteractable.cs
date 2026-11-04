using UnityEngine;

public class TimerInteractable : MonoBehaviour, IInteractable
{
    public GameObject[] connectedObject;
    bool isActive = false;
    public float timeLimit;
    float timer;

    public void Interact()
    {
        print("Button pressed! You have " + timeLimit + " seconds.");
        isActive = true;

        foreach (GameObject obj in connectedObject)
        {
            obj.SetActive(!obj.activeSelf);
        }

        TimeActivation();
    }
    void TimeActivation()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
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
    }
}
