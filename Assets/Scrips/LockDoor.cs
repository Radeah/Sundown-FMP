using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockDoor : MonoBehaviour
{
    public GameObject pickUpText;
    public GameObject keyItem;

    private bool isLocked = true;
    private bool isPlayerInTrigger = false;

    void Start()
    {
        SetPickupTextActive(false);
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
                    SetPickupTextActive(true);
                }
                else
                {
                    SetPickupTextActive(true);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            SetPickupTextActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked && InventoryManager.instance.HasItem(keyItem))
            {
                UnlockDoor();
            }
        }
    }

    void UnlockDoor()
    {
        isLocked = false;
        SetPickupTextActive(false);
        Destroy(gameObject, 2f);
    }

    void SetPickupTextActive(bool isActive)
    {
        pickUpText.SetActive(isActive);
        if (isActive)
        {
            Text textComponent = pickUpText.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.text = "Press E to unlock";
            }
            else
            {
                Debug.LogError("Text component not found on pickUpText GameObject.");
            }
        }
    }
}






