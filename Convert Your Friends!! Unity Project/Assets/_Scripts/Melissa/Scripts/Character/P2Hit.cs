using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Hit : MonoBehaviour {

    public float hitForce;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitForce = 100;
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
    }
}
