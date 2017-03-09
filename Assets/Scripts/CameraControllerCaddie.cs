using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerCaddie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxis("RJoystickX");
        Debug.Log(moveHorizontal);
    }
}
