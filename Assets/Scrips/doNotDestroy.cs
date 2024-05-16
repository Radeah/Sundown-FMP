using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        // Check if the GameObject has a parent
        if (transform.parent == null)
        {
            // Find all GameObjects with the "Music" tag
            GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Music");

            // If there is more than one "Music" GameObject, destroy this one
            if (musicObj.Length > 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                // Make this GameObject persistent across scenes
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            Debug.LogWarning("doNotDestroy script is attached to a non-root GameObject. Ensure it is a root GameObject for DontDestroyOnLoad to work.");
        }
    }
}

