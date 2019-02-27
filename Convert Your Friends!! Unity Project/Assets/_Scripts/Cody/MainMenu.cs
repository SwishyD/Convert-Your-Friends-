using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


   public void StartGame()
    {
        SceneManager.LoadScene("CodySandBox");
    }

   public void ControlScreen()
    {

    }

   public void QuitGame()
    {
        Application.Quit();
    }
}
