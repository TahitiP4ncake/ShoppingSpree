using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameObjectNormalize
{
    [MenuItem("Tools/Mesh -> GameObject %g")]
    private static void MeshToGameObject()
    {
        foreach (GameObject _s in Selection.gameObjects)
        {
            if (_s.GetComponent<MeshRenderer>() != null)
            {
                GameObject _parent = new GameObject();
                GameObject _visuals = new GameObject();
                GameObject _physics = new GameObject();

                _s.transform.parent = _visuals.transform;

                _visuals.transform.parent = _parent.transform;
                _physics.transform.parent = _parent.transform;

                _parent.name = _s.name;


                _visuals.name = "Visuals";
                _physics.name = "Physics";

                if (_s.GetComponent<Collider>() != null)
                {
                    //Collider _c = _s.GetComponent<Collider>();

                    GameObject _physics1 = new GameObject();
                    _physics1.AddComponent<Collider>();
					/*
                    Collider _ph1 = _physics1.GetComponent<Collider>();

                    _ph1 = _c;
					*/

                    //Editor.DestroyImmediate(_c);

                    _physics1.transform.parent = _physics.transform;

                    _physics1.name = _s.name + "Physics";



                }

                _parent.name = _parent.name.Remove(0, 3);
            }
        }
    }
}
