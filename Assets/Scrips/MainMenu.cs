using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    // Method to start the game by loading the next scene
    public void PlayGame()
    {
        // Assuming the main game scene is at index 1
        SceneManager.LoadScene(1);
    }

    // Method to load the settings scene
    public void LoadSettings()
    {
        // Load the settings scene by name
        SceneManager.LoadScene("Settings");
    }

    // Method to load the credits scene
    public void LoadCredits()
    {
        // Load the credits scene by name
        SceneManager.LoadScene("Credits");
    }

    // Method to quit the game
    public void QuitGame()
    {
        // Log to the console for debugging purposes
        Debug.Log("Quit");

        // Quit the application
        // Note: Application.Quit() does not work in the editor, only in a built application
        Application.Quit();

#if UNITY_EDITOR
        // Stop playing the scene in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}



