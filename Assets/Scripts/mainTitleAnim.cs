using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainTitleAnim : MonoBehaviour {

    Rigidbody rb;

    public float speed;
    public Transform spawn;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.Log(rb.velocity);
        //transform.position += new VEctor
        rb.velocity = new Vector3(speed, 0, 0);
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag =="depop")
        {
            transform.position = spawn.position;
        }
    }
}
