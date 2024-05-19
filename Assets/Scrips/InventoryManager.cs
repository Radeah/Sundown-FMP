using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Make the class a singleton accessible from other scripts
    public static InventoryManager instance;

    // List to hold inventory items
    private List<GameObject> inventoryItems = new List<GameObject>();

    private void Awake()
    {
        // Ensure only one instance of InventoryManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(GameObject item)
    {
        // Add item to the inventory
        inventoryItems.Add(item);

        // Disable the renderer or the entire GameObject
        Renderer[] renderers = item.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        // Log that the item has been added to the inventory
        Debug.Log(item.name + " has been added to the inventory.");
    }

    public void RemoveItem(GameObject item)
    {
        // Remove item from the inventory
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
    }

    public List<GameObject> GetInventoryItems()
    {
        // Return a list of inventory items
        return inventoryItems;
    }
}



