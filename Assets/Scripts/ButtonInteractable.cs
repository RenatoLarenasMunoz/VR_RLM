using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    //public GameObject connectedObject;
    public GameObject[] connectedObject;

    public void Interact()
    {
        Debug.Log("Button pressed!");

        foreach (GameObject obj in connectedObject)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
