using System.Linq;                          // AGREGADO: para filtrar dispositivos con OfType / Count
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;           // AGREGADO: clase XRController (los mandos del Quest)

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


    // =====================================================================
    // AGREGADO: arreglo para que los mandos del Quest funcionen con Player Input
    // =====================================================================
    //
    // EL PROBLEMA
    // Al darle Play, los mandos XR todavía NO existen como dispositivos: OpenXR los
    // registra unos instantes después, cuando arranca la sesión. El Player Input elige
    // su Control Scheme una sola vez, al activarse. Como en ese momento no hay mandos,
    // cae en "Keyboard&Mouse". Con ese esquema activo, la acción Move (cuyo único binding
    // es el thumbstick del grupo "XR") queda sin controles, y OnMove nunca se ejecuta,
    // aunque el Input Debugger sí vea el joystick moviéndose.
    //
    // LA SOLUCIÓN
    // Escuchar cuándo aparecen (o reaparecen) los mandos y, en ese momento, pedirle al
    // Player Input que cambie al esquema XR emparejando esos mandos de forma explícita.
    //
    // REQUISITOS EN EL INSPECTOR (Player Input del XR Origin)
    //  - Default Scheme: XR
    //  - Auto-Switch: DESACTIVADO. Si está activo, al mover el mouse el Player Input
    //    vuelve a Keyboard&Mouse y Move deja de funcionar.
    //  - Este script debe estar en el MISMO GameObject que el Player Input.

    [Header("XR Input (fix)")]
    [Tooltip("Nombre EXACTO del Control Scheme de XR en el asset de Input Actions.")]
    public string xrSchemeName = "XR";

    PlayerInput playerInput;

    void Awake()
    {
        // Buscamos el Player Input que está en este mismo GameObject.
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
            Debug.LogWarning("[Movement] No hay un Player Input en este GameObject. Los mandos XR no se emparejarán.");
    }

    void OnEnable()
    {
        // Nos avisan cada vez que Unity agrega, reconecta o cambia un dispositivo.
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void OnDisable()
    {
        // Siempre hay que desuscribirse para no dejar eventos colgados.
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        // Added: el mando aparece por primera vez.
        // Reconnected: el mando se había dormido o desconectado y volvió.
        // UsageChanged: el mando recibió su etiqueta LeftHand / RightHand.
        if (change == InputDeviceChange.Added ||
            change == InputDeviceChange.Reconnected ||
            change == InputDeviceChange.UsageChanged)
        {
            TrySwitchToXR();
        }
    }

    void TrySwitchToXR()
    {
        if (playerInput == null) return;

        // Todos los mandos XR que existen ahora mismo.
        XRController[] xrControllers = InputSystem.devices.OfType<XRController>().ToArray();
        if (xrControllers.Length == 0) return;   // Todavía no hay mandos: esperamos al próximo evento.

        // Si ya estamos en XR con todos los mandos emparejados, no hay nada que hacer.
        bool yaEmparejado = playerInput.currentControlScheme == xrSchemeName &&
                            playerInput.devices.Count(d => d is XRController) == xrControllers.Length;
        if (yaEmparejado) return;

        // Cambiamos al esquema XR y le indicamos con qué dispositivos trabajar.
        playerInput.SwitchCurrentControlScheme(xrSchemeName, xrControllers);
        Debug.Log($"[Movement] Esquema '{xrSchemeName}' activado con {xrControllers.Length} mando(s).");
    }
    // =====================================================================
    // FIN DE LO AGREGADO (más abajo solo hay UNA línea nueva, en Start)
    // =====================================================================


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // AGREGADO: por si los mandos ya existían cuando arrancó la escena.
        TrySwitchToXR();
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