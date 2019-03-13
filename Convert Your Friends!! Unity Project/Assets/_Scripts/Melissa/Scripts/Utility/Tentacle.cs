using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {

    Rigidbody rb;
    float originX;
    float originY;
    float newX;
    float newY;
    public int turnSpeed;


	void Start () {
        rb = GetComponent<Rigidbody>();
        originX = transform.position.x;
        originY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newAngle = transform.eulerAngles;
        newAngle.y += turnSpeed * 1 * Time.deltaTime;
        transform.eulerAngles = newAngle;
    }
}
