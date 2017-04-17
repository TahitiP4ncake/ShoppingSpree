using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RigidbodyDelete : ScriptableWizard {

    public string searchTag = "Created";

    [MenuItem("Tools/Rigidbody Delete")]
    static void SelectAllOfTagWizard()
    {
        ScriptableWizard.DisplayWizard<RigidbodyDelete>("delete tagged rigidbody", "Delete selected Rigidbodies");
    }

    void OnWizardCreate()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(searchTag);
        Selection.objects = gameObjects;
        foreach (GameObject objects in gameObjects)
        {
            DestroyImmediate(objects.GetComponent<Rigidbody>());
            objects.isStatic = true;           

        }
    }

}
