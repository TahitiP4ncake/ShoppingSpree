using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCamera : MonoBehaviour {
    public float TurnHSpeed = 2;
    public float BackSpeed = 1f;
    public Transform test;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //  Debug.Log(transform.rotation.y);
        float _rotateHorizontal = Input.GetAxis("RJoystickX");
        if (_rotateHorizontal > 0.5 || _rotateHorizontal < -0.5)
        {
            transform.Rotate(new Vector3(0, _rotateHorizontal * TurnHSpeed, 0));
            // transform.localEulerAngles = new Vector3(20, _rotateHorizontal, 0);
            //  transform.Rotate(new Vector3 (0,_rotateHorizontal,0));
        }
        
                if (transform.localEulerAngles.y > 60 && transform.localEulerAngles.y <180)
                {
                    transform.localEulerAngles = (Vector3.Lerp(new Vector3(0, transform.localEulerAngles.y, 0), new Vector3(0, 0, 0), BackSpeed*Time.deltaTime  ));
                    //transform.Rotate (Vector3.Lerp(new Vector3 (0, transform.rotation.y,0), new Vector3 (0,0,0), BackSpeed));
                }
                
 
        if (transform.localEulerAngles.y >180 && transform.localEulerAngles.y <300)
        {
          
            transform.localEulerAngles = (Vector3.Lerp(new Vector3(0, transform.localEulerAngles.y, 0), new Vector3(0, 360, 0), BackSpeed * Time.deltaTime));
            //transform.Rotate (Vector3.Lerp(new Vector3 (0, transform.rotation.y,0), new Vector3 (0,0,0), BackSpeed));
        }
        
        // Debug.Log(transform.localEulerAngles.y);


        /* float _rotateHorizontal = Input.GetAxis("RJoystickX");

         if (_rotateHorizontal > 0.5 || _rotateHorizontal < -0.5)
         {
             transform.Rotate(new Vector3(0, _rotateHorizontal * TurnHSpeed, 0));
             // transform.localEulerAngles = new Vector3(20, _rotateHorizontal, 0);
             //  transform.Rotate(new Vector3 (0,_rotateHorizontal,0));
         }
         if (transform.rotation.y > 60 || transform.rotation.y < -60)
         {
             Debug.Log("stoooop");
             //transform.Rotate (Vector3.Lerp(new Vector3 (0, transform.rotation.y,0), new Vector3 (0,0,0), BackSpeed));
         }
         */
        /*
         else
        {
            transform.localEulerAngles = new Vector3(20, 0, 0);
        }
        */
    }
}
