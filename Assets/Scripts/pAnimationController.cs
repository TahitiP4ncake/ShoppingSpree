using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pAnimationController : MonoBehaviour {

    public Rigidbody Caddie;
    public Animator Papa;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float _direction = (Input.GetAxis("LJoystickX"))*20;
       // Debug.Log(_direction);
        float _speed = Caddie.velocity.magnitude;
        Papa.SetFloat("velocity", _speed);
        Papa.SetFloat("direction", _direction);
	}
}
