using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "NPC")
        {
            GameManager.instance.P1Goal();
        }
    }
}
