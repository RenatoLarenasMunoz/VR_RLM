using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;


    //private void OnMouseDown()
    //{
    //    Interact();
    //}

    public void OnInteract(InputAction.CallbackContext _ctx)
    {
        if (_ctx.started)
        {
            Debug.Log(_ctx.started);
            Interact();
        }
    }

    void Interact()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
        {
            if(hitInfo.collider.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log(hitInfo);
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(interactorSource.position, interactorSource.forward * interactRange);
    }
}
