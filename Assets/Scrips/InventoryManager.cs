using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private List<GameObject> inventoryItems = new List<GameObject>();

    private void Awake()
    {
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
        inventoryItems.Add(item);
    }

    public bool HasItem(GameObject item)
    {
        return inventoryItems.Contains(item);
    }

    public void RemoveItem(GameObject item)
    {
        if (HasItem(item))
        {
            inventoryItems.Remove(item);
        }
    }

    public List<GameObject> GetInventoryItems()
    {
        return inventoryItems;
    }
}




