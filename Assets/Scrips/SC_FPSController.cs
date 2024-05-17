using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float sprintSpeed = 11.5f;
    public float sprintStaminaDepletionRate = 50f; // Increase depletion rate for faster depletion
    public float maxStamina = 100f; // Maximum stamina value
    public float crouchSpeed = 3.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float crouchStaminaRegenRate = 5f; // Rate at which stamina regenerates when crouching
    public float walkStaminaRegenRate = 10f; // Rate at which stamina regenerates when walking
    public float customStaminaDepletionRate = 50f; // Customizable stamina depletion rate

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float stamina; // Stamina variable

    [HideInInspector]
    public bool canMove = true;
    private float originalHeight;
    public float crouchHeight = 1.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalHeight = characterController.height;
        stamina = maxStamina; // Initialize stamina to maximum value
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0; // Sprinting when Shift is pressed and there's enough stamina
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);

        float curSpeedX = 0;
        float curSpeedY = 0;
        float staminaRegenRate = walkStaminaRegenRate; // Set default stamina regeneration rate to walk rate

        if (canMove)
        {
            if (isSprinting)
            {
                curSpeedX = sprintSpeed * Input.GetAxis("Vertical");
                curSpeedY = sprintSpeed * Input.GetAxis("Horizontal");
                // Deplete stamina while sprinting
                stamina -= sprintStaminaDepletionRate * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0f, maxStamina); // Clamp stamina value
            }
            else if (isCrouching)
            {
                curSpeedX = crouchSpeed * Input.GetAxis("Vertical");
                curSpeedY = crouchSpeed * Input.GetAxis("Horizontal");
                // Gain stamina when crouching, but at a slower rate
                staminaRegenRate = crouchStaminaRegenRate;
            }
            else
            {
                curSpeedX = walkingSpeed * Input.GetAxis("Vertical");
                curSpeedY = walkingSpeed * Input.GetAxis("Horizontal");
            }
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        if (canMove)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Crouch
        if (isCrouching && canMove)
        {
            characterController.height = crouchHeight;
        }
        else if (canMove)
        {
            characterController.height = originalHeight;
        }

        // Regenerate stamina
        if (!isSprinting && canMove)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina); // Clamp stamina value
        }
    }
}







