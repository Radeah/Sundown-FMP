using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public Text DisplayBox;
    public Text PassBox;
    public GameObject QTECanvas; // Reference to the Canvas or parent GameObject of the UI elements

    private int QTEGen;
    private bool waitingForKey;
    private float countdownTime;
    private float initialCountdownTime = 3.5f;
    private float minCountdownTime = 1f;
    private float difficultyIncrement = 0.2f;
    private int successfulKeyPresses;
    private int maxSuccessfulKeyPresses = 5;
    private bool qteStarted = false;

    void Start()
    {
        countdownTime = initialCountdownTime;
        QTECanvas.SetActive(false); // Start with the QTE UI hidden
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
        while (successfulKeyPresses < maxSuccessfulKeyPresses)
        {
            yield return new WaitForSeconds(2f); // Wait before generating the next QTE
            QTEGen = Random.Range(1, 4);
            waitingForKey = true;
            countdownTime = Mathf.Max(countdownTime, minCountdownTime);

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
        }

        DisplayBox.text = "QTE Sequence Complete!";
        yield return new WaitForSeconds(1.5f); // Wait for a short duration before hiding the UI
        QTECanvas.SetActive(false); // Turn off the UI
        qteStarted = false;
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
            countdownTime = Mathf.Max(countdownTime - difficultyIncrement, minCountdownTime);

            if (successfulKeyPresses >= maxSuccessfulKeyPresses)
            {
                DisplayBox.text = "QTE Sequence Complete!";
                StartCoroutine(EndQTESequence()); // Start coroutine to end the QTE sequence
                return;
            }
        }
        else
        {
            DisplayResult("FAILED");
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !qteStarted) // Assuming the player has the tag "Player"
        {
            QTECanvas.SetActive(true); // Show the QTE UI
            qteStarted = true;
            successfulKeyPresses = 0;
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
        }
    }
}

