using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //private Vector3 offset;
    public GameObject Car;
    public GameObject Muscle;

    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    //private bool dezoom;
    //private bool zoom;

    public GameObject generator;
    public Renderer R_car;
    public Renderer[] R_carList;
    public GameObject[] lesVoitures;
    public List<GameObject> voituresPosition;

   // public float ZoomOut;
	// Use this for initialization
	void Start () {

       // voituresPosition = new List<GameObject>();


             //offset = transform.position - Car.transform.position; 
    }
	void Update()
    {
        lesVoitures = GameObject.FindGameObjectsWithTag("Player");
        /*
        if(dezoom && transform.position.y<150)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x+1.2f, transform.position.y + 1, transform.position.z), ref velocity, 0.1f);
            //Debug.Log("peut etre");
        }
        if(zoom && transform.position.y >60)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x - 1.2f, transform.position.y - 1, transform.position.z), ref velocity, 0.1f);
            //Debug.Log("zoom");
        }
        */
        //foreach (GameObject Car in lesVoitures)
        // Debug.Log(lesVoitures);
        //voituresPosition = generator.GetComponent<List>();


        //Debug.Log(voituresPosition.Count);
        //  voituresPosition = generator.Voitures;
        //  Debug.Log(voituresPosition);
    }
	// Update is called once per frame
   
	void FixedUpdate () {
        //Vector3 leMilieu = transform.position;
        Vector3 leMilieu = Vector3.zero;
        foreach (GameObject Car  in lesVoitures)
        {
            
            leMilieu =leMilieu+ Car.transform.position;
            
            /*//Vector3.Distance(new Vector3(transform.position.x,0, transform.position.z),new Vector3(Car.transform.position.x,0, Car.transform.position.z));
            Debug.Log(Vector3.Distance(new Vector3(leMilieu.x, 0, leMilieu.z), new Vector3(Car.transform.position.x, 0, Car.transform.position.z)));
            if (Vector3.Distance(new Vector3(leMilieu.x, 0, leMilieu.z), new Vector3(Car.transform.position.x, 0, Car.transform.position.z)) > 60)
            {
                dezoom = true;
            }
            else
            {
                dezoom = false;
            }

            if (Vector3.Distance(new Vector3(leMilieu.x, 0, leMilieu.z), new Vector3(Car.transform.position.x, 0, Car.transform.position.z)) <50)
            {
                zoom = true;

            }
            else
            {
                zoom = false;
            }
            */
        }
        
            leMilieu = leMilieu / (GameObject.FindGameObjectsWithTag("Player").Length);
        int dehors = 0;
        int dedans = 0;
        
        foreach (GameObject Car in lesVoitures)
        {
            
            //Debug.Log(Car);
            Vector3 screenPos = GetComponent<Camera>().WorldToScreenPoint(Car.transform.position);
            //float test = Vector3.Distance(screenPos, transform.position);
            //Debug.Log(Screen.width / 100 * 80);
           // float _hauteur = Vector3.Distance(transform.position, leMilieu);
           // float _ecart = Vector3.Distance(new Vector3(leMilieu.x, 0, leMilieu.z), new Vector3(Car.transform.position.x, 0, Car.transform.position.z));
            //Debug.Log(Vector3.Distance(new Vector3(leMilieu.x, 0, leMilieu.z), new Vector3(Car.transform.position.x, 0, Car.transform.position.z)));
            
            if (screenPos.x<=( GetComponent<Camera>().pixelWidth/ 100*10)|| screenPos.x>= (GetComponent<Camera>().pixelWidth / 100*90)|| screenPos.y<= (GetComponent<Camera>().pixelHeight / 100*10)|| screenPos.y>=(GetComponent<Camera>().pixelHeight / 100*90))
            {
                //dezoom = true;
                Debug.Log("dezoom");
                //zoom = false;
                dehors += 1;
            }
            else
            {
                //dezoom = false;
            }
            
            if (screenPos.x>= (GetComponent<Camera>().pixelWidth / 100*30)&& screenPos.x<=( GetComponent<Camera>().pixelWidth / 100*70)&& screenPos.y>= (GetComponent<Camera>().pixelHeight / 100*30 )&& screenPos.y<= (GetComponent<Camera>().pixelHeight / 100*70))
            {
                dedans += 1;
                //zoom = true;
                Debug.Log("zoom");
               // dezoom = false;
            }
            else
            {
                //zoom = false;
            }
            
            /*
            if(Car.GetComponentInChildren<Renderer>().isVisible)
            {
                dezoom = true;
            }
            */
            
        }
    
        //leMilieu.x += 50;
        
        
        
        leMilieu.x += 50;
        //leMilieu.x = leMilieu.x / GameObject.FindGameObjectsWithTag("Player").Length;
        //leMilieu.z = leMilieu.z / GameObject.FindGameObjectsWithTag("Player").Length;
        targetPosition = leMilieu ;
        targetPosition.y = transform.position.y;
        //targetPosition.x = targetPosition.y;
         //targetPosition.x += targetPosition.y/2;
        //Debug.Log(targetPosition);
        //targetPosition.x = targetPosition.x + 30;

        if (dehors == 2 && transform.position.y < 150)
        {
            targetPosition = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x + 20f, transform.position.y + 10, transform.position.z), ref velocity, 0.5f);
            //Debug.Log("peut etre");
           // targetPosition.x += 20;
          //  targetPosition.y += 10;
        }
        if (dedans == 2 && transform.position.y > 60)
        {
           // targetPosition.x -= 20;
            //targetPosition.y -= 10;
            targetPosition = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x - 20f, transform.position.y - 10, transform.position.z), ref velocity, 0.5f);
            //Debug.Log("zoom");
        }

        if (lesVoitures.Length != 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        //transform.position = Car.transform.position + offset;

        
    }
    
}
