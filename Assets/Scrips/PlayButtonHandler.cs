using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public string targetButtonTag = "PlayButton"; // Tag assigned to the play button

    public void OnButtonClicked()
    {
        // Check if the clicked button has the correct tag
        if (gameObject.CompareTag(targetButtonTag))
        {
            // Load the next scene
            LoadNextScene();
        }
    }

    // Load the next scene in the build settings
    private void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the current scene index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

