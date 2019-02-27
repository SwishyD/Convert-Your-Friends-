using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;

    public bool gameIsPaused = false;
    
	void Start ()
    {
        pauseMenuUI.SetActive(false);
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused)
        {
            Resume();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    public void ControlScreen()
    {

    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    
}
