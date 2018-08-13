using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Explanation here
public class MenuTransition : MonoBehaviour {

    public Image overlayPanel;                              // Reference to Panel's Image component (Alpha)
    public GameObject overlayPanelObject;                   // Reference to Panel's GameObject properties (SetActive)

    public MainMenu mainMenu;

    // Event response to OnClick() for "Play"
    public void TransitionToLevel()     // Can pass message through argument "string message"
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        //Set the overlay Panel active and ready to fade
        overlayPanelObject.SetActive(true);

        for (float alpha = 0f; alpha < 1f; alpha += Time.deltaTime)
        {
            // Set new Alpha value for Panel's Color
            overlayPanel.color = new Color(overlayPanel.color.r, overlayPanel.color.g, overlayPanel.color.b, alpha);

            Debug.Log("Value: " + alpha);

            yield return null;
        }

        // After fade out, Start Game
        mainMenu.PlayGame();
    }
}