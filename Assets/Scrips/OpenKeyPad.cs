using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyPad : MonoBehaviour
{
    public GameObject keyPadObjv;
    public GameObject keyPadText;

    public bool inReach;

    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            keyPadText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            keyPadText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && inReach)
        {
            keyPadObjv.SetActive(true);
        }
    }
}
