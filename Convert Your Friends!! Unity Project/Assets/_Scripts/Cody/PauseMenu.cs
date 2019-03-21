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
            AudioManager.instance.Play("UIPress");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused)
        {
            Resume();
            AudioManager.instance.Play("UIPress");
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
        AudioManager.instance.Play("UIPress");
    }

    public void MenuConfirm()
    {
        controls.SetActive(false);
        menuConfirm.SetActive(true);
        pause.SetActive(false);
        AudioManager.instance.Play("UIPress");
    }

    public void MainMenu()
    {
        AudioManager.instance.Play("UIPress");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void Back()
    {
        AudioManager.instance.Play("UIPress");
        controls.SetActive(false);
        menuConfirm.SetActive(false);
        pause.SetActive(true);
    }

    public void ButtonHover()
    {
        AudioManager.instance.Play("UIHover");
    }
    
}
