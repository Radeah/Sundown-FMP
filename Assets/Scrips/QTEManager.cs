using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public Text DisplayBox;
    public Text PassBox;
    public GameObject QTECanvas; // Reference to the Canvas or parent GameObject of the UI elements
    public GameObject player; // Reference to the player GameObject
    private SC_FPSController playerController; // Reference to the player's movement controller script/component

    private int QTEGen;
    private bool waitingForKey;
    private bool qteInProgress; // Flag to track if a QTE sequence is in progress
    private float countdownTime;
    private float initialCountdownTime = 3.5f;
    private int successfulKeyPresses;
    private int maxSuccessfulKeyPresses = 5;
    private bool qteStarted = false;

    void Start()
    {
        countdownTime = initialCountdownTime;
        QTECanvas.SetActive(false); // Start with the QTE UI hidden
        playerController = FindObjectOfType<SC_FPSController>(); // Initialize playerController
    }

    void Update()
    {
        if (qteStarted && waitingForKey && Input.anyKeyDown)
        {
            CheckInput();
        }
    }

    IEnumerator GenerateQTE()
    {
        // Ensure only one QTE sequence is active at a time
        if (qteInProgress)
        {
            yield break;
        }

        qteInProgress = true;

        while (successfulKeyPresses < maxSuccessfulKeyPresses)
        {
            QTEGen = Random.Range(1, 4);
            waitingForKey = true;

            switch (QTEGen)
            {
                case 1:
                    DisplayBox.text = "[E]";
                    break;
                case 2:
                    DisplayBox.text = "[R]";
                    break;
                case 3:
                    DisplayBox.text = "[T]";
                    break;
            }

            StartCoroutine(Countdown());

            // Wait for the current QTE sequence to complete
            yield return new WaitForSeconds(3.5f);
        }

        DisplayBox.text = "Complete!";
        yield return new WaitForSeconds(1.5f); // Wait for a short duration before hiding the UI
        QTECanvas.SetActive(false); // Turn off the UI
        qteStarted = false;
        qteInProgress = false; // Reset the flag
        EnablePlayerMovement(); // Enable player movement after completing the QTE
        Destroy(gameObject); // Destroy the QTEManager GameObject
    }

    IEnumerator Countdown()
    {
        float timeRemaining = countdownTime;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        if (waitingForKey)
        {
            waitingForKey = false;
            DisplayResult("FAILED");
            yield return new WaitForSeconds(4f); // Wait for 4 seconds before restarting the QTE
            RestartQTE(); // Restart the QTE if the countdown ends without a key press
        }
    }

    void CheckInput()
    {
        bool correctKey = false;

        switch (QTEGen)
        {
            case 1:
                correctKey = Input.GetKeyDown(KeyCode.E);
                break;
            case 2:
                correctKey = Input.GetKeyDown(KeyCode.R);
                break;
            case 3:
                correctKey = Input.GetKeyDown(KeyCode.T);
                break;
        }

        waitingForKey = false;
        if (correctKey)
        {
            successfulKeyPresses++;
            DisplayResult("PASS");

            if (successfulKeyPresses >= maxSuccessfulKeyPresses)
            {
                DisplayBox.text = "Complete!";
                StartCoroutine(EndQTESequence()); // Start coroutine to end the QTE sequence
                return;
            }
        }
        else
        {
            DisplayResult("FAILED");
            StartCoroutine(RestartQTEWithDelay()); // Restart the QTE with a delay if the key pressed is incorrect
        }
    }

    void DisplayResult(string result)
    {
        PassBox.text = result;
        DisplayBox.text = "";
        StartCoroutine(ClearResult());
    }

    IEnumerator ClearResult()
    {
        yield return new WaitForSeconds(1.5f);
        PassBox.text = "";
    }

    IEnumerator EndQTESequence()
    {
        yield return new WaitForSeconds(1.5f);
        QTECanvas.SetActive(false);
        qteStarted = false;
        qteInProgress = false; // Reset the flag
        EnablePlayerMovement(); // Enable player movement after completing the QTE
        Destroy(gameObject); // Destroy the QTEManager GameObject
    }

    IEnumerator RestartQTEWithDelay()
    {
        yield return new WaitForSeconds(4f); // Wait for 4 seconds before restarting the QTE
        RestartQTE();
    }

    void RestartQTE()
    {
        successfulKeyPresses = 0; // Reset the successful key presses count
        StartCoroutine(GenerateQTE()); // Restart the QTE sequence
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !qteStarted) // Assuming the player has the tag "Player"
        {
            QTECanvas.SetActive(true); // Show the QTE UI
            qteStarted = true;
            successfulKeyPresses = 0;
            DisablePlayerMovement(); // Disable player movement when entering the trigger
            StartCoroutine(GenerateQTE());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && qteStarted)
        {
            StopAllCoroutines(); // Stop QTE sequence if the player leaves the trigger area
            QTECanvas.SetActive(false);
            qteStarted = false;
            DisplayBox.text = "";
            PassBox.text = "";
            EnablePlayerMovement(); // Enable player movement if the player exits the trigger area
        }
    }

    void DisablePlayerMovement()
    {
        if (playerController != null)
        {
            playerController.canMove = false; // Disable player movement
        }
    }

    void EnablePlayerMovement()
    {
        if (playerController != null)
        {
            playerController.canMove = true; // Enable player movement
        }
    }
}









