using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMove : MonoBehaviour {

    public NavMeshAgent agent;

    public Transform[] waypoints = new Transform[20];

    Vector3 destination;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        Move();
	}
	
	// Update is called once per frame
	void Update () {

        

		if(agent.remainingDistance <= 1.0f)
        {
            Move();           
        }
	}

    private void Move()
    {
        int nextWaypoint = Random.Range(0, 21);
        destination = waypoints[nextWaypoint].position;
        agent.SetDestination(destination);
    }
}
