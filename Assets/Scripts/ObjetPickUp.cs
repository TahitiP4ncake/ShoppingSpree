using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjetPickUp : MonoBehaviour {

   public Collider papa;
    private Rayon rayon;
    private GameObject objet;
    private float position;
    public GameObject coursesTransform;

    public List<GameObject> Courses = new List<GameObject>();

    void Start()
    {
        position = transform.position.y;
        Courses.Add(coursesTransform);
    }

    void OnCollisionEnter(Collision other)
    {
        rayon = other.gameObject.GetComponentInParent<Rayon>();
        if (other.collider.tag == "Rayon" && rayon.picked == false)
        {
            rayon.PickedUp();
            
            objet = Instantiate(rayon.objet, new Vector3(coursesTransform.transform.position.x, (rayon.objet.GetComponentInChildren<Renderer>().bounds.extents.y)*2 + position, coursesTransform.transform.position.z),transform.rotation);
            Courses.Add(objet as GameObject);
            position += (rayon.objet.GetComponentInChildren<Renderer>().bounds.extents.y)*2;
            //objet.transform.SetParent(gameObject.transform);
            objet.GetComponent<dragObject>().papa = Courses[Courses.Count - 2].transform;
            objet.GetComponent<dragObject>().index = Courses.Count;
        }
    }

    void Update()
    {
        //Debug.Log(Courses);
    }
}
