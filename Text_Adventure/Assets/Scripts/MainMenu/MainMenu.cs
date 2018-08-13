using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        // we can do this in several ways:
        // SceneManager.LoadScene("Level01");   By level name
        // SceneManager.LoadScene(1);           By level index
        
        // This way we load the next level in the queue (current scene + 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT Function invoked from MainMenu!");
        Application.Quit();
    }
}
