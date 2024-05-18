using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject invObj; // Reference to the inventory representation of the item
    public GameObject pickUpText;

    void Start()
    {
        pickUpText.SetActive(false);
        invObj.SetActive(false); // Make sure the inventory object is initially inactive
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        if (pickUpText.activeSelf && Input.GetButtonDown("E"))
        {
            InventoryManager.instance.AddItem(invObj); // Add the item to the inventory
            pickUpText.SetActive(false);
            Destroy(gameObject); // Destroy the pickup object
        }
    }
}

