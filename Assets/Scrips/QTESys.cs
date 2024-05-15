using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WaitingForKey  == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
        }

        if (QTEGen == 1)
        {
            WaitingForKey =  1;
        }













    }











}
