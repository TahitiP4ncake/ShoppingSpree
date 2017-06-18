using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderPosition : MonoBehaviour {

    private Vector3 offset;
    private Quaternion orientation;

    public GameObject papa;
	void Start () {
        offset = papa.transform.position - transform.position;
        orientation = transform.rotation * papa.transform.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = papa.transform.position+ offset;
        transform.rotation = papa.transform.rotation*orientation;
	}
}
