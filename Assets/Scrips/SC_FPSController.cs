using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float sprintSpeed = 11.5f;
    public float sprintStaminaDepletionRate = 50f;
    public float maxStamina = 100f;
    public float crouchSpeed = 3.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float crouchStaminaRegenRate = 5f;
    public float walkStaminaRegenRate = 10f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float stamina;

    [HideInInspector]
    public bool canMove = true;
    private float originalHeight;
    public float crouchHeight = 1.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalHeight = characterController.height;
        stamina = maxStamina;
    }

    void Update()
    {
        if (!canMove) return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0;
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);

        float curSpeedX = 0;
        float curSpeedY = 0;
        float staminaRegenRate = walkStaminaRegenRate;

        if (isSprinting)
        {
            curSpeedX = sprintSpeed * Input.GetAxis("Vertical");
            curSpeedY = sprintSpeed * Input.GetAxis("Horizontal");
            stamina -= sprintStaminaDepletionRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
        else if (isCrouching)
        {
            curSpeedX = crouchSpeed * Input.GetAxis("Vertical");
            curSpeedY = crouchSpeed * Input.GetAxis("Horizontal");
            staminaRegenRate = crouchStaminaRegenRate;
        }
        else
        {
            curSpeedX = walkingSpeed * Input.GetAxis("Vertical");
            curSpeedY = walkingSpeed * Input.GetAxis("Horizontal");
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        if (isCrouching)
        {
            characterController.height = crouchHeight;
        }
        else
        {
            characterController.height = originalHeight;
        }

        if (!isSprinting)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
    }

    // Disable movement when entering the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = false;
        }
    }

    // Enable movement when exiting the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = true;
        }
    }
}


