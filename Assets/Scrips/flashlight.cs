using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlightObject;
    public AudioSource turnOnSound;
    public AudioSource turnOffSound;

    private bool isFlashlightOn = false;

    void Start()
    {
        // Ensure flashlight is initially off
        TurnOffFlashlight();
    }

    void Update()
    {
        // Toggle flashlight when pressing the C key
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isFlashlightOn)
                TurnOffFlashlight();
            else
                TurnOnFlashlight();
        }
    }

    // Method to turn on the flashlight
    void TurnOnFlashlight()
    {
        flashlightObject.SetActive(true); // Activate the flashlight GameObject
        turnOnSound.Play(); // Play the sound for turning on the flashlight
        isFlashlightOn = true; // Set the flag indicating the flashlight is on
    }

    // Method to turn off the flashlight
    void TurnOffFlashlight()
    {
        flashlightObject.SetActive(false); // Deactivate the flashlight GameObject
        turnOffSound.Play(); // Play the sound for turning off the flashlight
        isFlashlightOn = false; // Set the flag indicating the flashlight is off
    }
}