using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    //public GameObject connectedObject;
    public GameObject[] connectedObject;

    public void Interact()
    {
        print("Button pressed!");

        foreach (GameObject obj in connectedObject)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
