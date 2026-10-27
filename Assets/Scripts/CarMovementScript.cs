using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovementScript : MonoBehaviour
{
    private float carSpeed = 5f;
    private float maxSpeed; // will add later on for applying actual car mechanics
    private float angle = 90f;

    PlayerInput carInput;
    InputAction carAction;
    InputAction carRotate;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carInput = GetComponent<PlayerInput>();
        carAction = carInput.actions.FindAction("Move");
        carRotate = carInput.actions.FindAction("Turn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveCar();
        RotateCar();
    }

    void MoveCar()
    {
        float Direction = carAction.ReadValue<Vector3>().z;
        Vector3 position = (transform.forward * Direction);
        rb.MovePosition(rb.position + position * carSpeed * Time.fixedDeltaTime);
    }

    void RotateCar()
    {
        float rotateDirection = carRotate.ReadValue<float>();
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotateDirection * angle * Time.fixedDeltaTime, 0f));
    }
}