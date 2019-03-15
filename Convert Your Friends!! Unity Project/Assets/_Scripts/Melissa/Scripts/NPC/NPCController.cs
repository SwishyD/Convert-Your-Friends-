using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {

    float hitForce;
    Rigidbody rb;
    public NPCMovement npcMove;
    bool forces;
    bool held;
    public NavMeshAgent navAgent;
    public GameObject[] hit;
    

    void Start () {
        rb = GetComponent<Rigidbody>();
        hitForce = 3000;
        forces = true;
        held = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnCollisionEnter(Collision col)
    {
        if (!held)
        {
            if ((col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2" || col.gameObject.tag == "Tentacle") && forces)
            {

                //Vector3 dir = col.contacts[0].point - transform.position;
                //dir = -dir.normalized;
                npcMove.ragdoll = true;
                //rb.AddForce(dir * hitForce);
                rb.AddForce(Vector3.up * hitForce);
                rb.AddForce(col.gameObject.transform.forward * hitForce);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!held)
        {
            if (other.tag == "HandJoint")
            {
                CharacterJoint charHand = other.GetComponent<CharacterJoint>();
                if (charHand.connectedBody == null)
                {
                    gameObject.layer = other.gameObject.layer;
                    held = true;
                    forces = false;
                    navAgent.enabled = false;
                    npcMove.grabbed = true;
                    charHand.connectedBody = rb;
                }
            }
        }
    }
}
