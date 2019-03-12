using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    private float hitForce;
    Rigidbody rb;
    public NPCMovement npcMove;

    void Start () {
        rb = GetComponent<Rigidbody>();
        hitForce = 5000;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2" || col.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            //Vector3 dir = col.contacts[0].point - transform.position;
            //dir = -dir.normalized;
            npcMove.ragdoll = true;
            //rb.AddForce(dir * hitForce);
            rb.AddForce(Vector3.up * 5000);
            rb.AddForce(col.gameObject.transform.forward * hitForce);
            
        }
    }
}
