using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public CharacterJoint handJoint;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "NPC" && handJoint.connectedBody == null)
        {
            handJoint.connectedBody = col.GetComponent<Rigidbody>();
        }
    }
}
