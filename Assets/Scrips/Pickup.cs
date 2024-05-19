using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject itemToAdd; // Reference to the item to add to the inventory
    public GameObject pickUpText; // Reference to the UI text for interaction
    public bool isInUse = false; // Flag to track whether the item is in use or not

    void Start()
    {
        SetPickupTextActive(false); // Ensure the text is initially hidden
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInUse)
        {
            SetPickupTextActive(true); // Show the interaction text
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPickupTextActive(false); // Hide the interaction text
        }
    }

    void Update()
    {
        if (!isInUse && pickUpText.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            AddItemToInventory();
        }
    }

    void AddItemToInventory()
    {
        InventoryManager.instance.AddItem(itemToAdd); // Add the item to the inventory
        Destroy(gameObject); // Destroy the pickup object after adding it to the inventory
        SetPickupTextActive(false); // Hide the interaction text
    }

    void SetPickupTextActive(bool isActive)
    {
        pickUpText.SetActive(isActive);
        if (isActive)
        {
            // Set the text to display "Press E"
            pickUpText.GetComponent<TextMesh>().text = "Press E";
        }
    }
}


