using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    // Public variables for customization
    public float moveSpeed = 3.0f;
    public float detectionRange = 10.0f;
    public int health = 100;

    // Reference to the player
    private Transform player;

    // Initialize variables
    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If player is within detection range, chase
        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
    }

    // Move the monster towards the player
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to handle the monster's death
    void Die()
    {
        // Destroy the monster object
        Destroy(gameObject);
    }

    // Method to handle collisions
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // Check if collision involves the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collision detected. Loading Death scene...");
            // Load the "Death" scene
            SceneManager.LoadScene("Death");
        }
    }
}





