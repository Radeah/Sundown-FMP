using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndRotatePlayer : MonoBehaviour
{
    private SC_FPSController playerController;
    public Transform lookAtTarget; // Reference to the object to look at

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<SC_FPSController>();
            if (playerController != null)
            {
                RotatePlayerLeft();
                Destroy(gameObject); // Destroy the collider game object
            }
        }
    }

    private void RotatePlayerLeft()
    {
        playerController.transform.Rotate(Vector3.up, -90.0f); // Rotate player 90 degrees to the left immediately

        if (lookAtTarget != null)
        {
            // Calculate direction to the target
            Vector3 direction = lookAtTarget.position - playerController.transform.position;
            direction.y = 0f; // Keep the direction in the horizontal plane
            // Rotate player to look at the target
            playerController.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}


