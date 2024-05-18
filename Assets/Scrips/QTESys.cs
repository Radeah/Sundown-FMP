using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QTESys : MonoBehaviour
{
    public GameObject DisplayBox;
    public GameObject PassBox;
    public int QTEGen;
    public int CorrectKey;
    public int CountingDown;
    public int WaitingForKey;

    // Update is called once per frame
    void Update()
    {
        // Check if DisplayBox and PassBox are assigned
        if (DisplayBox == null)
        {
            Debug.LogError("DisplayBox is not assigned!");
            return;
        }

        if (PassBox == null)
        {
            Debug.LogError("PassBox is not assigned!");
            return;
        }

        if (WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());

            switch (QTEGen)
            {
                case 1:
                    DisplayBox.GetComponent<Text>().text = "[E]";
                    break;
                case 2:
                    DisplayBox.GetComponent<Text>().text = "[T]";
                    break;
                case 3:
                    DisplayBox.GetComponent<Text>().text = "[Y]";
                    break;
            }

            WaitingForKey = 1;
        }

        if (WaitingForKey == 1 && Input.anyKeyDown)
        {
            bool correct = false;

            switch (QTEGen)
            {
                case 1:
                    correct = Input.GetKeyDown(KeyCode.E);
                    break;
                case 2:
                    correct = Input.GetKeyDown(KeyCode.R);
                    break;
                case 3:
                    correct = Input.GetKeyDown(KeyCode.T);
                    break;
            }

            CorrectKey = correct ? 1 : 2;
            StartCoroutine(Keypressing());
        }
    }

    IEnumerator Keypressing()
    {
        QTEGen = 4;
        if (CorrectKey == 1)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "PASS";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
        else if (CorrectKey == 2)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "FAILED";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3.5f);
        if (CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "FAILED";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }
}

