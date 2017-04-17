using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class fruitSpawner : ScriptableWizard {
    [Space]
    public GameObject spawnerSize;
    public GameObject Fruit;
   
    public int numberOfFruits;
 
    [MenuItem("Tools/Fruit Spawner ")]
    static void CreateWizard()
    {

        ScriptableWizard.DisplayWizard<fruitSpawner> ("Create Fruits", "Drop them", "Spawn Fruits");

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
        for (int i = 0; i < numberOfFruits; i++)
        {
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = spawnerSize.transform.TransformPoint(rndPosWithin * .5f);

            GameObject _fruit = Instantiate(Fruit, rndPosWithin, spawnerSize.transform.rotation);
            //_fruit.GetComponent<Rigidbody>().WakeUp();
            //Debug.Log("WAKE UP");
            //_fruit.GetComponent<Rigidbody>().AddForce(Vector3.down,ForceMode.VelocityChange);
            _fruit.tag = "Created";
            


        }


    }
}
