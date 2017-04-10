using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPosition : MonoBehaviour {
    public GameObject[] lesVoitures;
    public GameObject Car;


    private Vector3 leMilieu;
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lesVoitures = GameObject.FindGameObjectsWithTag("Player");
    }
    void FixedUpdate()
    {
        Vector3 leMilieu = Vector3.zero;

        foreach (GameObject Car in lesVoitures)
        {

            leMilieu = leMilieu + Car.transform.position;
        }

        leMilieu = leMilieu / (GameObject.FindGameObjectsWithTag("Player").Length);

        targetPosition = leMilieu;


        if (lesVoitures.Length != 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
