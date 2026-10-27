using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public GameManager gm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nivel incompleto, intentalo de nuevo");
            gm.GameOver();
        }
    }
}
