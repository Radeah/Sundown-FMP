using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private List<GameObject> inventoryItems;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            inventoryItems = new List<GameObject>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(GameObject item)
    {
        inventoryItems.Add(item);
        item.SetActive(false); // Deactivate item in the scene
        Debug.Log("Item added to inventory: " + item.name);
    }

    public bool HasItem(GameObject item)
    {
        return inventoryItems.Contains(item);
    }

    // Optionally, you can add methods to remove items, use items, etc.
}

