 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCThrust : MonoBehaviour {

    float thrust = 20;
    private void OnTriggerStay(Collider col)
    {
        col.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust);
    }
}
