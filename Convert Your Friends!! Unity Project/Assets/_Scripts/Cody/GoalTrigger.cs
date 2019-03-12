using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {

    public bool player1Goal;

    public GameObject[] p1Particles;
    public GameObject[] p2Particles;
    public Transform npcResPoint;

    private void OnTriggerEnter(Collider col)
    { 
        //When an NPC gets pushed into a pit//
        if(col.gameObject.tag == "NPC" && player1Goal)
        {

            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.P1Goal();
            col.transform.position = npcResPoint.position;
        }
        else if (col.gameObject.tag == "NPC" && !player1Goal)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.P2Goal();
            col.transform.position = npcResPoint.position;
        }

        //If players fall into pits//
        if(col.gameObject.tag == "Player1" && !player1Goal)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.Player2Sacrifice();
        }
        else if (col.gameObject.tag == "Player1" && player1Goal)
        {
            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.RespawnP1();
        }
        if (col.gameObject.tag == "Player2" && player1Goal)
        {
            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.Player1Sacrifice();
        }
        else if (col.gameObject.tag == "Player2" && !player1Goal)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.RespawnP2();
        }
    }
}
