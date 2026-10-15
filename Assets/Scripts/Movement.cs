using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundMask;
    bool isGrounded;

    public Transform orientation;

    Vector2 direction;

    Vector3 moveDirection;

    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundMask);

        SpeedControl();

        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(direction);
    }

    public void OnMove(InputAction.CallbackContext _ctx)
    {
        print(_ctx.phase);
        direction = _ctx.ReadValue<Vector2>();

        if (_ctx.canceled)
        {
            direction = Vector2.zero;
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void MovePlayer(Vector2 _direction)
    {
        Vector3 orientationForward = new Vector3(orientation.forward.x, 0, orientation.forward.z).normalized;
        Vector3 orientationRight = new Vector3(orientation.right.x, 0, orientation.right.z).normalized;
        moveDirection = orientationForward * _direction.y + orientationRight * _direction.x;  
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
