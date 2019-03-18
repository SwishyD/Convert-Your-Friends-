using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Hit : MonoBehaviour {

    float hitForce;
    float tentacleForce;
    Rigidbody rb;
    public CharacterMovement charMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitForce = 500;
        tentacleForce = 1000;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player1")
        {
            Vector3 dir = col.contacts[0].point - transform.position;
            dir = -dir.normalized;
            col.gameObject.GetComponent<Rigidbody>().AddForce(dir * hitForce);
        }
        if (col.gameObject.tag == "Tentacle")
        {
            Vector3 dir = col.contacts[0].point - transform.position;
            dir = -dir.normalized;
            charMove.ragdoll = true;
            rb.AddForce(dir * tentacleForce);
            rb.AddForce(Vector3.up * tentacleForce);
        }
    }
}
