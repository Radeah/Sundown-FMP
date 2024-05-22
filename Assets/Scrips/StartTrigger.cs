using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public TaskJuggling taskJuggling; // Reference to the TaskJuggling script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate platforms immediately
            taskJuggling.StartJuggling();
        }
    }
}

