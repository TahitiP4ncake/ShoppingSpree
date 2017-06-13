using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjetPickUp : MonoBehaviour {

   public Collider papa;
    private Rayon rayon;
    private GameObject objet;
    private float position;
    public GameObject coursesTransform;

    public CoursesManager manager;

    public List<GameObject> Courses = new List<GameObject>();

    public float Price;
    public Text priceText;

    private float vegP;
    private float canP;
    private float botP;

    private float origin;
    public AnimationCurve priceCurve;
    private float priceAnima;

    void Start()
    {
        origin = priceText.transform.position.y;
        priceText.text = "";
        position = coursesTransform.transform.position.y;
        Courses.Add(coursesTransform);
        vegP = Random.Range(2, 10)-0.01f;
        canP = Random.Range(10, 20) - 0.01f;
        botP = Random.Range(5, 15) - 0.01f;
    }

    void Update()
    {
        if (priceAnima > 0)
        {
            priceText.transform.position = new Vector3(priceText.transform.position.x, origin + priceCurve.Evaluate(priceAnima), priceText.transform.position.z);
            priceAnima += Time.deltaTime;
            if (priceCurve.Evaluate(priceAnima) <= 0)
            {
                priceAnima = 0;
                
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        rayon = other.gameObject.GetComponentInParent<Rayon>();
        if (other.collider.tag == "Rayon" && rayon.picked == false)
        {
            rayon.PickedUp();
            
            objet = Instantiate(rayon.objet, new Vector3(coursesTransform.transform.position.x, (rayon.objet.GetComponentInChildren<Renderer>().bounds.extents.y) + position, coursesTransform.transform.position.z),transform.rotation);
            Courses.Add(objet as GameObject);
            position += (rayon.objet.GetComponentInChildren<Renderer>().bounds.extents.y);
            //objet.transform.SetParent(gameObject.transform);
            objet.GetComponent<dragObject>().papa = Courses[Courses.Count - 2].transform;
            objet.GetComponent<dragObject>().index = Courses.Count;
            SetLayerRecursively(objet, 8);
            objet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            if(objet.tag ==manager.carottesG.tag)
            {
                manager.carottesO += 1;
                manager.UpdateListe();
            }
            if (objet.tag == manager.tomatesG.tag)
            {
                manager.tomatesO += 1;
                manager.UpdateListe();
            }
            if (objet.tag == manager.sodaG.tag)
            {
                manager.sodaO += 1;
                manager.UpdateListe();
            }
            if (objet.tag == manager.ananasG.tag)
            {
                manager.ananasO += 1;
                manager.UpdateListe();
            }
            if (objet.tag == manager.cassouletG.tag)
            {
                manager.cassouletO += 1;
                manager.UpdateListe();
            }
            if (objet.tag == manager.poisG.tag)
            {
                manager.poisO += 1;
                manager.UpdateListe();
            }
            manager.ObjectOnTheList(objet);
        }
    }

    public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    public void WinScreen()
    {
        Price = manager.carottesO * vegP +
                manager.tomatesO * vegP +
                manager.sodaO * botP +
                manager.ananasO * canP +
                manager.cassouletO * canP +
                manager.poisO * canP;
        priceText.text = Mathf.Floor(Price*100)/100 + " €";
        priceAnima = 0.001f;

    }
}
