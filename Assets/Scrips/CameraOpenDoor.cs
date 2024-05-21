using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraDoorScript
{
    public class CameraOpenDoor : MonoBehaviour
    {
        public float DistanceOpen = 3;
        public GameObject text;

        // Use this for initialization
        void Start()
        {
            text.SetActive(false); // Ensure the text is initially hidden
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
            {
                if (hit.transform.GetComponent<DoorScript.Door>())
                {
                    text.SetActive(true);
                    // Check for "E" key or Space key press to open the door
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.E))
                    {
                        hit.transform.GetComponent<DoorScript.Door>().OpenDoor();
                    }
                }
                else
                {
                    text.SetActive(false);
                }
            }
            else
            {
                text.SetActive(false);
            }
        }
    }
}
