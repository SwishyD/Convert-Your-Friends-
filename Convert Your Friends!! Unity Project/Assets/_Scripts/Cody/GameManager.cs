using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Score//
    public int p1Score = 0;
    public int p2Score = 0;
    public Text p1ScoreText;
    public Text p2ScoreText;

    //Players//
    public GameObject p1;
    public GameObject p2;
    public Transform p1ResPoint;
    public Transform p2ResPoint;


    //Time//
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

    void Start()
    {
        RespawnP1();
        RespawnP2();
        p1Score = 0;
        p2Score = 0;
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

    //For changing the score UI//
    void HandleScore()
    {
        p1ScoreText.text = "" + p1Score;
        p2ScoreText.text = "" + p2Score;

    }

    //When a player gets an NPC into their pit//
    public void P1Goal()
    {
        p1Score++;
        Debug.Log(p1Score);
    }
    public void P2Goal()
    {
        p2Score++;
    }

    //When P1 Gets P2 into P1s Pit//
    public void Player1Sacrifice()
    {
        p1Score += 5;
        RespawnP2();
    }
    //Vice Versa from above//
    public void Player2Sacrifice()
    {
        p1Score += 5;
        RespawnP1();
    }

    //When a player goes out of bounds or into a pit//
    public void RespawnP1()
    {
        p1.transform.position = p1ResPoint.position;
    }
    public void RespawnP2()
    {
        p2.transform.position = p2ResPoint.position;
    }

    //For when the Timer reaches 0//
    void RoundEnd()
    {
        SceneManager.LoadScene("CodySandBox");

        if(p1Score > p2Score)
        {
            Player1Win();
        }
        else if(p2Score > p1Score)
        {
            Player2Win();
        }
        else if(p1Score == p2Score)
        {
            Draw();
        }
    }

    void Player1Win()
    {

    }
    void Player2Win()
    {

    }
    void Draw()
    {

    }
}
