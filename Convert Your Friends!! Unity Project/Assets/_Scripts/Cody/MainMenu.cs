using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject controls;
    public GameObject menu;
    public GameObject quitConfirm;
    public GameObject levelSelect;
    public LevelLoader levelLoader;

    public void StartGame()
    {
        AudioManager.instance.Play("UIPress");
        controls.SetActive(false);
        quitConfirm.SetActive(false);
        menu.SetActive(false);
        levelSelect.SetActive(true);
    }

   public void ControlScreen()
    {
        AudioManager.instance.Play("UIPress");
        controls.SetActive(true);
        quitConfirm.SetActive(false);
        menu.SetActive(false);
        levelSelect.SetActive(false);
    }

    public void QuitConfirm()
    {
        AudioManager.instance.Play("UIPress");
        controls.SetActive(false);
        quitConfirm.SetActive(true);
        menu.SetActive(false);
        levelSelect.SetActive(false);
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
        levelSelect.SetActive(false);
        AudioManager.instance.Play("UIPress");
    }

    public void ButtonHover()
    {
        AudioManager.instance.Play("UIHover");
    }

    public void Pit()
    {
        AudioManager.instance.Play("UIPress");
        levelLoader.LoadLevel(1);
    }

    public void Island()
    {
        AudioManager.instance.Play("UIPress");
        levelLoader.LoadLevel(2);
    }

}
