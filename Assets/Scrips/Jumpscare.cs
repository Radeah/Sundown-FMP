using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    public GameObject JumpscareObject;

    private bool hasTriggeredJumpscare = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the JumpscareObject has not been triggered yet and the collision is with the player
        if (!hasTriggeredJumpscare && other.CompareTag("Player"))
        {
            TriggerJumpscare();
        }
    }

    void TriggerJumpscare()
    {
        // Check if the JumpscareObject is not null
        if (JumpscareObject != null)
        {
            // Show the JumpscareObject
            JumpscareObject.SetActive(true);

            // Set the flag to indicate that the JumpscareObject has been triggered
            hasTriggeredJumpscare = true;

            // Destroy the JumpscareObject after 2 seconds
            Destroy(JumpscareObject, 1f);
        }
    }
}