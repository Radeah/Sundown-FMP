using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isActive = false;
    private bool isCorrectPlatform = false; // Flag to track if this is the correct platform
    public AudioClip[] activationClips;  // Array of audio clips for platform activation sounds
    public AudioClip correctJumpClip;    // Audio clip for correct jump
    public AudioClip incorrectJumpClip;  // Audio clip for incorrect jump
    public AudioClip missedJumpClip;     // Audio clip for missed jump
    public float activationDuration = 5f; // Duration the platform stays active

    private Coroutine activationCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetColor(Color.gray);  // Initial color of the platform
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the platform!"); // Debug message for player touching the platform
            if (isActive)
            {
                if (isCorrectPlatform)
                {
                    TaskJuggling.instance.PlatformJumped(this);
                    PlaySound(correctJumpClip);
                    DeactivatePlatform();
                }
                else
                {
                    TaskJuggling.instance.PlatformMissed(this);
                    PlaySound(incorrectJumpClip);
                    DeactivatePlatform();
                }
            }
            else
            {
                TaskJuggling.instance.PlatformMissed(this);
                PlaySound(incorrectJumpClip);
            }
        }
    }

    public void ActivatePlatform(bool correct)
    {
        isActive = true;
        isCorrectPlatform = correct; // Set whether this is the correct platform or not
        SetColor(Random.ColorHSV());  // Change to a random color
        PlayRandomActivationSound();
        activationCoroutine = StartCoroutine(PlatformActivationTimer());
    }

    public void DeactivatePlatform()
    {
        isActive = false;
        isCorrectPlatform = false;
        SetColor(Color.gray);
        if (activationCoroutine != null)
        {
            StopCoroutine(activationCoroutine);
            activationCoroutine = null;
        }
    }

    private void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    private void PlayRandomActivationSound()
    {
        if (audioSource && activationClips.Length > 0)
        {
            int randomIndex = Random.Range(0, activationClips.Length);
            audioSource.clip = activationClips[randomIndex];
            audioSource.Play();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private IEnumerator PlatformActivationTimer()
    {
        yield return new WaitForSeconds(activationDuration);
        if (isActive)
        {
            TaskJuggling.instance.PlatformMissed(this);
            PlaySound(missedJumpClip);
            DeactivatePlatform();
        }
    }
}




