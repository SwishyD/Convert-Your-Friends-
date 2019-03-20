using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;
    public GameObject pause;
    public GameObject controls;
    public GameObject menuConfirm;

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
        pause.SetActive(true);
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        controls.SetActive(false);
        menuConfirm.SetActive(false);
        pause.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    public void ControlScreen()
    {
        controls.SetActive(true);
        menuConfirm.SetActive(false);
        pause.SetActive(false);
    }

    public void MenuConfirm()
    {
        controls.SetActive(false);
        menuConfirm.SetActive(true);
        pause.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        controls.SetActive(false);
        menuConfirm.SetActive(false);
        pause.SetActive(true);
    }
    
}
