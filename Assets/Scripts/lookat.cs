﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour {
    public Transform target;
    // Use this for initialization
    void Start () {
		
	}



    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target 
        transform.LookAt(target);
    }
}
