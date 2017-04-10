using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    public GameObject[] lesVoitures;
    public GameObject Car;
    public GameObject parent;

    private Vector3 leMilieu;
    private float zoom;
    //private float smoothTime = 0.3f;
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
        zoom = 0;
        foreach (GameObject Car in lesVoitures)
        {
            zoom += (Vector3.Distance(new Vector3(parent.transform.position.x, 0, parent.transform.position.z), new Vector3(Car.transform.position.x, 0, Car.transform.position.z)));
            if (Car.GetComponentInChildren<Renderer>().isVisible == false)
            {
                zoom += 1;

            }
        }
        zoom = zoom / (GameObject.FindGameObjectsWithTag("Player").Length);
        //Debug.Log(zoom);
        zoom = zoom / 10;
        /*
        if(Car.GetComponentInChildren<Renderer>().isVisible==false)
        {
            zoom += 2;

        }
        */

         if(lesVoitures.Length != 0)
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(10+(zoom*zoom)/6,10+(zoom*zoom)/6,transform.localPosition.z),ref velocity, 0.1f);
    }
}
