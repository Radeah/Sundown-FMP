using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{

    public Transform myCredit;
    public Vector3 movement;

    void Update()
    {
        myCredit.position += movement * Time.deltaTime;
    }
}
