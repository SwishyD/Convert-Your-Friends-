using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject controls;
    public GameObject menu;
    public GameObject quitConfirm;

    public void StartGame()
    {
        AudioManager.instance.Play("UIPress");
        SceneManager.LoadScene(1);
    }

   public void ControlScreen()
    {
        AudioManager.instance.Play("UIPress");
        controls.SetActive(true);
        quitConfirm.SetActive(false);
        menu.SetActive(false);
    }

    public void QuitConfirm()
    {
        AudioManager.instance.Play("UIPress");
        controls.SetActive(false);
        quitConfirm.SetActive(true);
        menu.SetActive(false);
    }

   public void QuitGame()
    {
        AudioManager.instance.Play("UIPress");
        Application.Quit();

    }

    public void Back()
    {
        controls.SetActive(false);
        quitConfirm.SetActive(false);
        menu.SetActive(true);
        AudioManager.instance.Play("UIPress");
    }

    public void ButtonHover()
    {
        AudioManager.instance.Play("UIHover");
    }
    
}
