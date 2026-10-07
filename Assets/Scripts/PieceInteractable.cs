using UnityEngine;

public class PieceInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameManager gm;


    public void Interact()
    {
        Debug.Log("Interaccion pieza");

        gm.CollectPiece();
        this.gameObject.SetActive(!gameObject.activeSelf);

        Debug.Log("posees " + gm.piecesCollected + " piezas");
    }
}
