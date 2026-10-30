using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovementScript : MonoBehaviour
{
    private float carSpeed = 0f;
    private float maxSpeed = 5f; // will add later on for applying actual car mechanics
    private float acceleration = 5f;
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
        Debug.Log(carSpeed);
    }

    void MoveCar()
    {
        float Direction = carAction.ReadValue<Vector3>().z;
        if ((Direction != 0) && (transform.rotation.z != 90 || transform.rotation.z != -90))
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

    void RotateCar()
    {
        if (carSpeed == 0f && (transform.rotation.z == 90 || transform.rotation.z == -90)) return;
        float rotateDirection = carRotate.ReadValue<float>();
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotateDirection * (carSpeed * 0.25f) * angle * Time.fixedDeltaTime, 0f));
    }
}