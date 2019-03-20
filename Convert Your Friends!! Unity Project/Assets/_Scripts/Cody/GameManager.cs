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
    float totalTime = 180f;
    public GameObject timerUI;

    //End of Game UI//
    public GameObject p1WinText;
    public GameObject p2WinText;
    public GameObject drawText;


    //NPC Respawning//
    public GameObject NPCPrefab;
    public Transform npcResPoint;
    public int spawned = 0;
    float spawntimer = 0f;
    float timeTospawn = 3f;
    public GameObject[] npcParticles;

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
        

        spawntimer += Time.deltaTime;
        if(spawntimer > timeTospawn)
        {
            spawntimer = 0;
            StartCoroutine(SpawnTime());
        }
       
	}

    

    public void NPCSpawn()
    {
        for (int i = 0; i < npcParticles.Length; i++)
        {
            npcParticles[i].GetComponent<ParticleSystem>().Play();            
        }
        GameObject clone;
        clone = Instantiate(NPCPrefab, npcResPoint);
    }

    //Controls the timer//
    void ManageTime()
    {
        timerUI.GetComponent<Image>().fillAmount = currentTime / totalTime;
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
        AudioManager.instance.Play("Explosion");
        p1Score++;        
    }
    public void P2Goal()
    {
        AudioManager.instance.Play("Explosion");
        p2Score++;
    }

    //When P1 Gets P2 into P1s Pit//
    public void Player1Sacrifice()
    {
        AudioManager.instance.Play("Explosion");
        p1Score += 3;
        RespawnP2();
    }
    //Vice Versa from above//
    public void Player2Sacrifice()
    {
        AudioManager.instance.Play("Explosion");
        p2Score += 3;
        RespawnP1();
    }

    //When a player goes out of bounds or into a pit//
    public void RespawnP1()
    {
        AudioManager.instance.Play("Explosion");
        p1.transform.position = p1ResPoint.position;
    }
    public void RespawnP2()
    {
        AudioManager.instance.Play("Explosion");
        p2.transform.position = p2ResPoint.position;
    }

    //For when the Timer reaches 0//
    void RoundEnd()
    {
        

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

        StartCoroutine(EndGame());
    }

    void Player1Win()
    {
        Time.timeScale = 0.1f;
        p1WinText.SetActive(true);
    }
    void Player2Win()
    {
        Time.timeScale = 0.1f;
        p2WinText.SetActive(true);
    }
    void Draw()
    {
        Time.timeScale = 0.1f;
        drawText.SetActive(true);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator SpawnTime()
    {
        if (spawned < 15)
        {
            yield return new WaitForSeconds(3);
            spawned++;
            NPCSpawn();
        }
        else
        {
            yield return null;
        }
    }
}
