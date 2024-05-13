using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    public Image staminaBar;
    private float initialWidth; // Initial width of the stamina bar
    private RectTransform staminaBarRect; // RectTransform of the stamina bar

    void Start()
    {
        // Get the RectTransform of the stamina bar
        if (staminaBar != null)
        {
            staminaBarRect = staminaBar.rectTransform;
            // Set the initial width of the stamina bar
            initialWidth = staminaBarRect.rect.width;
        }
        else
        {
            Debug.LogError("Stamina Bar reference is not set in the StaminaBarUI script!");
        }
    }

    public void UpdateStaminaBar(float staminaNormalized)
    {
        if (staminaBar != null && staminaBarRect != null)
        {
            // Calculate the new width based on stamina level
            float newWidth = initialWidth * staminaNormalized;

            // Update the width of the stamina bar
            staminaBarRect.sizeDelta = new Vector2(newWidth, staminaBarRect.sizeDelta.y);
        }
        else
        {
            Debug.LogError("Stamina Bar reference or RectTransform is null in the UpdateStaminaBar method!");
        }
    }



}
