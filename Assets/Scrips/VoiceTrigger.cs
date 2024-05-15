using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    public AudioClip voiceLine;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(voiceLine, transform.position);
            hasPlayed = true;
        }
    }
}