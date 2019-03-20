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
        SceneManager.LoadScene(1);
    }

   public void ControlScreen()
    {
        controls.SetActive(true);
        quitConfirm.SetActive(false);
        menu.SetActive(false);
    }

    public void QuitConfirm()
    {
        controls.SetActive(false);
        quitConfirm.SetActive(true);
        menu.SetActive(false);
    }

   public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        controls.SetActive(false);
        quitConfirm.SetActive(false);
        menu.SetActive(true);
    }
}
