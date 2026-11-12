using UnityEngine;

public class PieceInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameManager gm;
    public GameObject[] objects;


    public void Interact()
    {
        print("Interaccion pieza");

        gm.CollectPiece();
        foreach (var item in objects)
        {
            item.SetActive(!item.activeSelf);
        }

        Debug.Log("posees " + gm.piecesCollected + " piezas");
    }
}
