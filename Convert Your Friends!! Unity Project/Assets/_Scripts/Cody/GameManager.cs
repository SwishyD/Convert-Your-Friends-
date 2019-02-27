using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Score
    public int p1Score = 0;
    public int p2Score = 0;
    public Text p1ScoreText;
    public Text p2ScoreText;



    //Time
    float currentTime;
    float totalTime = 60;
    public Text timerText;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        currentTime = totalTime;
    }

   	
	void Update ()
    {
        ManageTime();
        HandleScore();
	}


    //Controls the timer//
    void ManageTime()
    {
        timerText.text = "" + Mathf.RoundToInt(currentTime);
        currentTime -= Time.deltaTime;

        if(currentTime > totalTime)
        {
            currentTime = totalTime;
        }

        if(currentTime <= 0f)
        {
            RoundEnd();
        }

    }

    //For changing the score UI
    void HandleScore()
    {
        p1ScoreText.text = "" + p1Score;
        p2ScoreText.text = "" + p2Score;

    }

    public void P1Goal()
    {
        p1Score++;
        Debug.Log(p1Score);
    }

    public void P2Goal()
    {
        p2Score++;
    }

    //For when the Timer reaches 0
    void RoundEnd()
    {
        SceneManager.LoadScene("CodySandBox");

       /* if(p1Score > p2Score)
        {

        }
        else if(p2Score > p1Score)
        {

        }
        else if(p1Score == p2Score)
        {

        }*/
    }
}
