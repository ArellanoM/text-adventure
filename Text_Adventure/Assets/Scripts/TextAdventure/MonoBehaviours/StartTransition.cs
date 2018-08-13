using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTransition : MonoBehaviour
{
    public Image overlayPanel;                              // Reference to Panel's Image component (Alpha)
    public GameObject overlayPanelObject;                   // Reference to Panel's GameObject properties (SetActive)

    // Event response to OnClick() for "Play"
    public void TransitionPanelFadeIn()     // Can pass message through argument "string message"
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        for (float alpha = 1f; alpha > 0f; alpha -= Time.deltaTime)
        {
            // Set new Alpha value for Panel's Color
            overlayPanel.color = new Color(overlayPanel.color.r, overlayPanel.color.g, overlayPanel.color.b, alpha);

            Debug.Log("Value: " + alpha);

            yield return null;
        }

        //Set the overlay Panel inactive
        overlayPanelObject.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        TransitionPanelFadeIn();
    }
	
}
