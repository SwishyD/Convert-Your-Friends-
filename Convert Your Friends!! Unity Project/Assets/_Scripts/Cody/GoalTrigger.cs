using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {

    public bool player1Goal;
    public bool killBox;

    public NPCMovement npcMove;

    public GameObject[] p1Particles;
    public GameObject[] p2Particles;
    public Transform npcResPoint;

    private void OnTriggerEnter(Collider col)
    { 
        //When an NPC gets pushed into a pit//
        if(col.gameObject.tag == "NPC" && player1Goal && !killBox)
        {

            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.P1Goal();
            NpcRes(col);
        }
        if (col.gameObject.tag == "NPC" && !player1Goal && !killBox)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.P2Goal();
            NpcRes(col);
        }

        //If players fall into pits//
        if(col.gameObject.tag == "Player1" && !player1Goal && !killBox)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.Player2Sacrifice();
        }
        if (col.gameObject.tag == "Player1" && player1Goal && !killBox)
        {
            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.RespawnP1();
        }
        if (col.gameObject.tag == "Player2" && player1Goal && !killBox)
        {
            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.Player1Sacrifice();
        }
        if (col.gameObject.tag == "Player2" && !player1Goal && !killBox)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
            }
            GameManager.instance.RespawnP2();
        }

        if(col.gameObject.tag == "NPC" && killBox)
        {
            NpcRes(col);
        }
        if (col.gameObject.tag == "Player1" && killBox)
        {
            GameManager.instance.RespawnP1();
        }
        if (col.gameObject.tag == "Player2" && killBox)
        {
            GameManager.instance.RespawnP2();
        }

       
    }
    public void NpcRes(Collider col)
    {
        col.gameObject.GetComponent<NPCMovement>().ragdoll = true;
        col.transform.position = npcResPoint.position;
    }
}
