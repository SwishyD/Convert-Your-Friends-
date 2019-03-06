﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Hit : MonoBehaviour {

    public float hitForce;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitForce = 10000;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player2")
        {
            Vector3 dir = col.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * hitForce);
        }
    }
}
