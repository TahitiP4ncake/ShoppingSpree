using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    public Animator papa;

	void Start () {
        papa.SetBool("walking",true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
