using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCamera : MonoBehaviour {
    public float TurnVSpeed = 2;
    public float BackSpeed = 1;
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
       // Debug.Log(transform.localEulerAngles.x);
        //  330  70
        /*  else
          {
              transform.localEulerAngles = new Vector3(20, 0, 0);
          }
            if (transform.localEulerAngles.y >180 && transform.localEulerAngles.y <300)
          {

              transform.localEulerAngles = (Vector3.Lerp(new Vector3(0, transform.localEulerAngles.y, 0), new Vector3(0, 360, 0), BackSpeed * Time.deltaTime));
              //transform.Rotate (Vector3.Lerp(new Vector3 (0, transform.rotation.y,0), new Vector3 (0,0,0), BackSpeed));
          }
          */
        if (transform.localEulerAngles.x < 345 && transform.localEulerAngles.x >180 )
        {

            transform.localEulerAngles = (Vector3.Lerp(new Vector3(transform.localEulerAngles.x, 0, 0), new Vector3(360, 0, 0), BackSpeed * Time.deltaTime));
            //transform.Rotate (Vector3.Lerp(new Vector3 (0, transform.rotation.y,0), new Vector3 (0,0,0), BackSpeed));
        }
        if (transform.localEulerAngles.x >70  && transform.localEulerAngles.x < 180)
        {

            transform.localEulerAngles = (Vector3.Lerp(new Vector3(transform.localEulerAngles.x, 0, 0), new Vector3(0, 0, 0), BackSpeed * Time.deltaTime));
            //transform.Rotate (Vector3.Lerp(new Vector3 (0, transform.rotation.y,0), new Vector3 (0,0,0), BackSpeed));
        }
    }
}
