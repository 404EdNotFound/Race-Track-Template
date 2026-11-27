using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovementScript : MonoBehaviour
{
    private float carSpeed = 0f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 5f;

    private float angle = 90f;

    PlayerInput carInput;
    InputAction carAction;
    InputAction carRotate;
    InputAction spawnAction;
    InputAction markPointAction;
    InputAction spawnPointAction;
    InputAction assignWayPoint;
    Rigidbody rb;

    KeyboardToggle keyboardToggle;

    public bool onGround;
    public bool onTrack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialises all components
        rb = GetComponent<Rigidbody>();
        carInput = GetComponent<PlayerInput>();
        carAction = carInput.actions.FindAction("Move");
        carRotate = carInput.actions.FindAction("Turn");
        spawnAction = carInput.actions.FindAction("Spawn");
        markPointAction = carInput.actions.FindAction("MarkPoint");
        spawnPointAction = carInput.actions.FindAction("ResetPoint");
        assignWayPoint = carInput.actions.FindAction("AssignWayPoint");

        keyboardToggle = FindAnyObjectByType<KeyboardToggle>();
}

    private void FixedUpdate()
    {
        // Used to call movements and keyboard triggers wherever possible
        MoveCar();
        RotateCar();
        keyboardTriggers();
    }

    // Collision triggers as the Car as a Box Collider, used to check whether the car is on the track or the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { onGround = true; }

        if (collision.gameObject.CompareTag("Track")) { onTrack = true; }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { onGround = false; }

        if (collision.gameObject.CompareTag("Track")) { onTrack = false; }
    }

    // Function used to move the car based on constant acceleration and limited speed
    void MoveCar()
    {
        float Direction = carAction.ReadValue<Vector3>().z;
        if ((Direction != 0) && (onGround || onTrack))
        {
            carSpeed += acceleration * Direction * Time.fixedDeltaTime;
            carSpeed = Mathf.Clamp(carSpeed, -maxSpeed, maxSpeed);
        }

        else
        {
            carSpeed = Mathf.MoveTowards(carSpeed, 0f, acceleration * Time.fixedDeltaTime);
        }

        Vector3 position = (transform.forward * carSpeed);
        rb.MovePosition(rb.position + position * Time.fixedDeltaTime);

    }

    // Used for rotation of the car!
    void RotateCar()
    {
        if (carSpeed == 0f) return;
        float rotateDirection = carRotate.ReadValue<float>();
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotateDirection * (carSpeed * 0.1f) * angle * Time.fixedDeltaTime, 0f));
    }

    // Keyboard triggers that are used ofr when a certain key is pressed and calls another script to perform these actions
    void keyboardTriggers()
    {
        if (spawnAction.IsPressed() && (!onGround && onTrack)) {
            keyboardToggle.spawnPitStop(rb.position, rb.rotation);
        }

        if (markPointAction.IsPressed() && (!onGround && onTrack)) {
            keyboardToggle.assignResetPoint(gameObject);
        }

        if (assignWayPoint.IsPressed() && (!onGround && onTrack)) {
            keyboardToggle.assignWayPoint(gameObject);
        }

        if (spawnPointAction.IsPressed() || (!onGround && !onTrack)) {
            keyboardToggle.respawnPoint();
        }
    }
}