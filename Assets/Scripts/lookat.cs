using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour {
    private Transform target;
    // Use this for initialization
    void Start () {
        target = Camera.main.transform;
	}



    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target 
        transform.LookAt(target);
    }
}
