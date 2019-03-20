using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {

    public CameraShake cameraShake;

    public bool player1Goal;
    public bool killBox;
        
    public GameObject[] p1Particles;
    public GameObject[] p2Particles;
     
    

    private void OnTriggerEnter(Collider col)
    { 
        //When an NPC gets pushed into a pit//
        if(col.gameObject.tag == "NPCHitbox" && player1Goal && !killBox)
        {

            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
                StartCoroutine(cameraShake.Shake(.15f, .4f));
            }
            GameManager.instance.P1Goal();
            NpcRes(col);
        }
        if (col.gameObject.tag == "NPCHitbox" && !player1Goal && !killBox)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
                StartCoroutine(cameraShake.Shake(.15f, .4f));
            }
            GameManager.instance.P2Goal();
            NpcRes(col);
        }

        //If players fall into pits//
        if(col.gameObject.tag == "P1Hitbox" && !player1Goal && !killBox)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
                StartCoroutine(cameraShake.Shake(.15f, .4f));
            }
            GameManager.instance.Player2Sacrifice();
        }
        if (col.gameObject.tag == "P1Hitbox" && player1Goal && !killBox)
        {
            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
                StartCoroutine(cameraShake.Shake(.15f, .4f));
            }
            GameManager.instance.RespawnP1();
        }
        if (col.gameObject.tag == "P2Hitbox" && player1Goal && !killBox)
        {
            for (int i = 0; i < p1Particles.Length; i++)
            {
                p1Particles[i].GetComponent<ParticleSystem>().Play();
                StartCoroutine(cameraShake.Shake(.15f, .4f));
            }
            GameManager.instance.Player1Sacrifice();
        }
        if (col.gameObject.tag == "P2Hitbox" && !player1Goal && !killBox)
        {
            for (int i = 0; i < p2Particles.Length; i++)
            {
                p2Particles[i].GetComponent<ParticleSystem>().Play();
                StartCoroutine(cameraShake.Shake(.15f, .4f));
            }
            GameManager.instance.RespawnP2();
        }

        if(col.gameObject.tag == "NPCHitbox" && killBox)
        {
            NpcRes(col);
        }
        if (col.gameObject.tag == "P1Hitbox" && killBox)
        {
            GameManager.instance.RespawnP1();
        }
        if (col.gameObject.tag == "P2hitbox" && killBox)
        {
            GameManager.instance.RespawnP2();
        }

       
    }
    public void NpcRes(Collider col)
    {
        Destroy(col.transform.parent.gameObject.transform.parent.gameObject);
        GameManager.instance.spawned--;
    }
}
