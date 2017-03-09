using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DudeGenerator : MonoBehaviour {

    public GameObject bouteille;

    public GameObject peinture;

    public int numberOfDudes = 5;

  

    public GameObject caddie;

	// Use this for initialization
	void Start () {
        //CreateABunchOfDudes();
  //      scoreManager = GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Createbouteille(transform.position, transform.rotation);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Createpeinture(transform.position, transform.rotation);
        }
    }

   void CreateABunchOfDudes()
    {
        for(int i=0; i<numberOfDudes;i++)
        {
            float x = Random.Range(-7.5f, 7.5f);
            float y = Random.Range(-7.5f, 7.5f);
            float u = Random.Range(-180, 180);
            
            Vector3 _position = new Vector3(x, 1, y);
            Quaternion _orientation = Quaternion.Euler(new Vector3(0,u,0));

            
            Createbouteille( _position,_orientation);
        }
    }
    

    GameObject Createbouteille(Vector3 _position,Quaternion _orientation)
    {
        GameObject _bouteilleInstance;
        _bouteilleInstance = Instantiate(bouteille, _position, _orientation) as GameObject;
      //  _bouteilleInstance.GetComponent<bouteille>().caddie = caddie;
     //   _dudeInstance.GetComponent<bouteille().bounceTrigger.scoreManager = scoreManager;
        return _bouteilleInstance;
    }
    GameObject Createpeinture(Vector3 _position, Quaternion _orientation)
    {
        GameObject _peintureInstance;
        _peintureInstance = Instantiate(peinture, _position, _orientation) as GameObject;
        //  _bouteilleInstance.GetComponent<bouteille>().caddie = caddie;
        //   _dudeInstance.GetComponent<bouteille().bounceTrigger.scoreManager = scoreManager;
        return _peintureInstance;
    }


}
