using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject pickUpText; // Reference to the UI text for interaction
    public GameObject keyItem; // Reference to the key item required to unlock the door

    private bool isLocked = true; // Flag to track whether the door is locked or unlocked
    private bool isPlayerInTrigger = false; // Flag to track whether the player is in the collider

    void Start()
    {
        SetPickupTextActive(false); // Ensure the text is initially hidden
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isLocked)
        {
            isPlayerInTrigger = true;
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
        if (isLocked && isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && InventoryManager.instance.HasItem(keyItem))
        {
            UnlockDoor(); // Unlock the door if the player has the key and presses E
        }
    }

    void UnlockDoor()
    {
        isLocked = false;
        SetPickupTextActive(false); // Hide the interaction text
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



