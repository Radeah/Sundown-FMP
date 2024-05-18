using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load

    private bool hasLoaded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasLoaded && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
            hasLoaded = true;
        }
    }
}
