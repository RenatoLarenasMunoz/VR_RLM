using Unity.VisualScripting;
using UnityEngine;

public class ExitLevelTrigger : MonoBehaviour
{
    [SerializeField] GameManager gm;

    [SerializeField] GameObject endTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && gm.exitOn == true)
        {
            Debug.Log("Nivel completado, felicidades");
            gm.EndLevel();
        }
    }
}
