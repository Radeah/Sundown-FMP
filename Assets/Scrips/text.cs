using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    SC_FPSController playerController;
    public GameObject uiObject;
    bool setTimer = false;
    float time = 5.0f;

    void Start()
    {
        playerController = FindObjectOfType<SC_FPSController>();
        uiObject.SetActive(false);
    }

    private void Update()
    {
        if (setTimer)
        {
            time -= Time.deltaTime;
            if (time < 0f)
            {
                playerController.canMove = true;
                Debug.Log("Player movement restored");
                Destroy(gameObject);
                Destroy(uiObject);
            }
        }
    }

    // Called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.canMove = false; // Stop player movement
            uiObject.SetActive(true); // Activate UI object
            setTimer = true; // Start timer
            RotatePlayerLeft(); // Rotate the player left
        }
    }

    // Rotate the player to the left
    void RotatePlayerLeft()
    {
        playerController.transform.Rotate(Vector3.up, -90.0f);
    }
}






