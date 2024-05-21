using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoorScript;

public class LockDoor : MonoBehaviour
{
    public GameObject pickUpText; // Reference to the UI text for interaction
    public GameObject keyItem; // Reference to the key item required to unlock the door

    private bool isLocked = true; // Flag to track whether the door is locked or unlocked
    private bool isPlayerInTrigger = false; // Flag to track whether the player is in the collider

    private Door door; // Reference to the Door script

    void Start()
    {
        SetPickupTextActive(false); // Ensure the text is initially hidden
        door = GetComponent<Door>(); // Get the Door script attached to the same GameObject
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (isLocked)
            {
                if (InventoryManager.instance.HasItem(keyItem))
                {
                    SetPickupTextActive(true); // Show the interaction text to unlock the door
                }
                else
                {
                    SetPickupTextActive(true); // Show the interaction text that the door is locked
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            SetPickupTextActive(false); // Hide the interaction text
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked && InventoryManager.instance.HasItem(keyItem))
            {
                UnlockDoor(); // Unlock the door if the player has the key and presses E
            }
        }
    }

    void UnlockDoor()
    {
        isLocked = false;
        SetPickupTextActive(false); // Hide the interaction text
        door.OpenDoor(); // Call the OpenDoor method from the Door script
        Destroy(gameObject); // Destroy the lock object itself
    }

    void SetPickupTextActive(bool isActive)
    {
        pickUpText.SetActive(isActive);
        if (isActive)
        {
            // Set the text to display "Press E to unlock"
            pickUpText.GetComponent<TextMesh>().text = "Press E to unlock";
        }
    }
}



