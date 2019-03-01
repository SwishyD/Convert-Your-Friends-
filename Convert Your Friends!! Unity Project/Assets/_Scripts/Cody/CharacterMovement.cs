using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float speed;
    public float lift;

    // Use this for initialization
    void Start () {
    

}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.position += new Vector3(-speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.position += new Vector3(speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(0, 0, -speed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(0, 0, speed);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.transform.position += new Vector3(0, speed, 0);
        }

    }
}
