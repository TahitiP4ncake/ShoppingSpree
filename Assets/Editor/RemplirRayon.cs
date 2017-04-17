using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RemplirRayon : ScriptableWizard {
    [Space]
    public GameObject spawnerSize;
    public GameObject Article;
    
    public int numberOfThings;
    public float spaceBetween;
    public bool randomRotation;

    [MenuItem("Tools/Remplir Rayon ")]
    static void CreateWizard()
    {

        ScriptableWizard.DisplayWizard<RemplirRayon>("Create Stuff", "Drop them", "Spawn Stuff");

    }

    void OnWizardCreate()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Created");
        Selection.objects = gameObjects;
        foreach (GameObject objects in gameObjects)
        {
            objects.GetComponent<Rigidbody>().WakeUp();


        }
    }

    void OnWizardOtherButton()
    {
        for (int i = 1; i < numberOfThings + 1; i++)
        {
            Vector3 rndPosWithin;
            rndPosWithin = spawnerSize.transform.position;
            rndPosWithin.x = (-(spawnerSize.transform.localScale.x/ numberOfThings) + spaceBetween*i)+ Random.Range(-.05f, .05f); ;
            //rndPosWithin.z = -(spawnerSize.transform.localScale.z / numberOfThings) + spaceBetween * i;
            Vector3 _rotation = spawnerSize.transform.eulerAngles;

            if(randomRotation)
            {
                _rotation.y = _rotation.y + Random.Range(0f, 360f);
            }
            Quaternion _qRotation = Quaternion.Euler(new Vector3(0,_rotation.y,0));
            //Vector3 objectSize = Vector3.Scale(spawnerSize.transform.localScale, spawnerSize.GetComponent<Mesh>().bounds.size);
            //rndPosWithin = spawnerSize.transform.TransformPoint(((objectSize.x)/numberOfThings+spaceBetween)*i,0,0);
            /*
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = spawnerSize.transform.TransformPoint(rndPosWithin * .5f);
            */
            GameObject _fruit = Instantiate(Article, rndPosWithin, _qRotation);
            //_fruit.GetComponent<Rigidbody>().WakeUp();
            //Debug.Log("WAKE UP");
            //_fruit.GetComponent<Rigidbody>().AddForce(Vector3.down,ForceMode.VelocityChange);
            _fruit.tag = "Created";



        }


    }

}
