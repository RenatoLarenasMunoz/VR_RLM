using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    //public GameObject connectedObject;
    public GameObject[] connectedObject;
    public AudioSource source;
    public AudioClip clip;

    public void Interact()
    {
        print("Button pressed!");

        foreach (GameObject obj in connectedObject)
        {
            obj.SetActive(!obj.activeSelf);
        }
        source.PlayOneShot(clip);
    }
}
