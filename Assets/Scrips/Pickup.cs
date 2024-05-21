using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject itemToAdd;
    public GameObject pickUpText;
    public bool isInUse = false;

    void Start()
    {
        SetPickupTextActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInUse)
        {
            SetPickupTextActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPickupTextActive(false);
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
        InventoryManager.instance.AddItem(itemToAdd);
        Destroy(gameObject);
        SetPickupTextActive(false);
    }

    void SetPickupTextActive(bool isActive)
    {
        pickUpText.SetActive(isActive);
        if (isActive)
        {
            Text textComponent = pickUpText.GetComponentInChildren<Text>(); // Get the Text component
            if (textComponent != null)
            {
                textComponent.text = "Press E"; // Set the text to display "Press E"
            }
            else
            {
                Debug.LogError("Text component not found on pickUpText GameObject or its children.");
            }
        }
    }
}



