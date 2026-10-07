using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    Vector2 direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnPoint(InputAction.CallbackContext _ctx)
    {
        direction = _ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
    }

    void CameraControl()
    {
        float mouseX = direction.x * Time.deltaTime * sensX;
        float mouseY = direction.y * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
