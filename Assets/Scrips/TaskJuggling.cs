using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskJuggling : MonoBehaviour
{
    public List<GameObject> platforms;
    public float minDelay = 1f;
    public float maxDelay = 3f;

    void Start()
    {
        StartCoroutine(JugglePlatforms());
    }

    IEnumerator JugglePlatforms()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            int randomIndex = Random.Range(0, platforms.Count);
            platforms[randomIndex].GetComponent<Renderer>().material.color = Color.white; // Reset color
            yield return new WaitForSeconds(0.1f); // Small delay before changing color
            platforms[randomIndex].SendMessage("ChangeColor", Random.ColorHSV(), SendMessageOptions.DontRequireReceiver);
        }
    }
}
