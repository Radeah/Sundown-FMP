using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TaskJuggling : MonoBehaviour
{
    public static TaskJuggling instance;

    public List<Platform> platforms;  // List of all platform objects
    public GameObject rewardObject;   // Object that appears after all platforms are jumped on
    public GameObject keyObject;      // Object that spawns when the player finishes
    public int requiredJumps = 5;     // Number of successful jumps required for the reward

    private int successfulJumps = 0;
    private bool isJuggling = false;

    void Awake()
    {
        instance = this;
        rewardObject.SetActive(false);  // Hide the reward object initially
        keyObject.SetActive(false);     // Hide the key object initially
    }

    public void StartJuggling()
    {
        if (!isJuggling)
        {
            isJuggling = true;
            SpawnNextPlatform();
        }
    }

    private void SpawnNextPlatform()
    {
        if (platforms.Count == 0)
        {
            Debug.LogWarning("No platforms assigned to the platforms list.");
            return;
        }

        int randomIndex = Random.Range(0, platforms.Count);
        Platform platform = platforms[randomIndex];
        platform.ActivatePlatform(true); // Pass true as parameter for correct platform
    }

    public void PlatformJumped(Platform platform)
    {
        successfulJumps++;
        if (successfulJumps >= requiredJumps)
        {
            rewardObject.SetActive(true); // Show the reward object
            keyObject.SetActive(true);    // Spawn the key object
            isJuggling = false; // Stop juggling

            Debug.Log("Congratulations! You have successfully jumped on all platforms."); // Debug log message
        }
        else
        {
            SpawnNextPlatform(); // Spawn the next platform
        }
    }

    public void PlatformMissed(Platform platform)
    {
        // Reset the game state if the player misses a platform
        successfulJumps = 0;
        SpawnNextPlatform(); // Restart the game
    }
} 




