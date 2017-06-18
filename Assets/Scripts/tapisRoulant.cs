using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapisRoulant : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer ==8)
        {
            Rigidbody _rb = other.GetComponentInParent<Rigidbody>();
           _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, speed); 
        }
    }
}
