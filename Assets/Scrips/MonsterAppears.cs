using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAppears : MonoBehaviour
{
    public GameObject objectToAppear;

    private bool hasAppeared = false;

    void Start()
    {
        // Ensure the objectToAppear is initially deactivated
        objectToAppear.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasAppeared && other.CompareTag("Player"))
        {
            // Activate the objectToAppear when the player enters the trigger
            objectToAppear.SetActive(true);
            hasAppeared = true;

            // Destroy the objectToAppear after 1 second
            Destroy(objectToAppear, 1f);
        }
    }
}

