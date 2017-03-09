using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCameraNop : MonoBehaviour {
    public float TurnVSpeed = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float _rotateVertical = Input.GetAxis("RJoystickY") ;
       // Debug.Log(_rotateVertical);
        if (Input.GetAxis("RJoystickY") > 0.5 || Input.GetAxis("RJoystickY") < -0.5)
        {
            // transform.localEulerAngles = new Vector3(_rotateVertical+20, 0, 0);
           transform.Rotate(new Vector3(_rotateVertical * TurnVSpeed,0,0));
        }
      /*  else
        {
            transform.localEulerAngles = new Vector3(20, 0, 0);
        }
        */
    }
}
