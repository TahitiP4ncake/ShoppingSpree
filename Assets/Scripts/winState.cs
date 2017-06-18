using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winState : MonoBehaviour {

    public ObjetPickUp papa;
    public Transform winTransform;

    public Transform POV;
    public timer timer;
    public bool winOn;
    private Vector3 velocity = Vector3.zero;

    
    public Image timerI;
    public Text timerT;
    public Image listeI;
    public Text listeT;
    public Text run;
    public Text priceT;

    private float price;
    public Transform papaPosition;
    public GameObject papaGO;
    private bool moving;

    public CoursesManager manager;

    public CaddieController papaC;

    private float priceAnima;
    public AnimationCurve priceCurve;
    private float origin;

    void Start()
    {
        priceT.enabled = false;
       origin = priceT.transform.position.y;
    }

   // private List<GameObject> layerObjects = new List<GameObject>();
    void Update () {
        /*
		if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Win());
        }
        */

        if(moving)
        {
            papaGO.transform.position = Vector3.Lerp(papaGO.transform.position, papaPosition.position, 0.01f * Time.time);
            papaGO.transform.rotation = Quaternion.Slerp(papaGO.transform.rotation, papaPosition.rotation, 0.01f * Time.time);
        }

        if (priceAnima > 0)
        {
            priceT.transform.position = new Vector3(priceT.transform.position.x, origin + priceCurve.Evaluate(priceAnima), priceT.transform.position.z);
            priceAnima += Time.deltaTime;
            if (priceCurve.Evaluate(priceAnima) <= 0)
            {
                priceAnima = 0;

            }
        }

        priceT.text = Mathf.Floor(price * 100) / 100 + " $";

    }

    GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }


    public IEnumerator Win()
    {
        timer.enabled = false;
        timerI.enabled = false;
        timerT.enabled = false;
        listeI.enabled = false;
        listeT.enabled = false;
        run.enabled = false;
        winOn = true;
        moving = true;
        Camera.main.transform.SetParent(POV);
        Camera.main.GetComponent<papaCamera>().GameOn = false;
        //Camera.main.transform.position = Vector3.SmoothDamp(transform.position, POV.position, ref velocity, 0.1f);
        Camera.main.transform.position = POV.position;
        Camera.main.transform.rotation = POV.rotation;
        
        yield return new WaitForSecondsRealtime(0.5f);
        priceT.enabled = true;
        foreach (GameObject course in papa.Courses)
        {
            if (course.GetComponent<dragObject>() != null)
            {
                course.GetComponent<dragObject>().enabled = false;
            }
            Destroy(course);
            //papa.Courses.RemoveAt(papa.Courses.Count);
            GameObject objet = Instantiate(course, winTransform);
            if(CheckStuff(objet)==1)
            {
                price += Random.Range(2, 10) - 0.01f;
            }
            if (CheckStuff(objet) == 2)
            {
                price += Random.Range(5, 15) - 0.01f;
            }
            if (CheckStuff(objet) == 3)
            {
                price += Random.Range(10, 20) - 0.01f;
            }
            priceAnima = 0.001f;
            if (objet.GetComponent<dragObject>() != null)
            {
                objet.GetComponent<dragObject>().enabled = false;
            }
            objet.transform.position = winTransform.position;
            objet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            objet.GetComponent<Rigidbody>().velocity = winTransform.forward*2;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        StartCoroutine(papaC.restartCD());
    }


    int CheckStuff(GameObject objet)
    {
        if (objet.tag == manager.carottesG.tag)
        {
            return 1;
        }
        else if (objet.tag == manager.tomatesG.tag)
        {
            return 1;
        }
        else if(objet.tag == manager.sodaG.tag)
        {
            return 2;
        }
        else if(objet.tag == manager.ananasG.tag)
        {
            return 3;
        }
        else if(objet.tag == manager.cassouletG.tag)
        {
            return 3;
        }
        else if(objet.tag == manager.poisG.tag)
        {
            return 3;
        }
        else if(objet.tag == manager.aubergineG.tag)
        {
            return 1;
        }
        else if(objet.tag == manager.baguetteG.tag)
        {
            return 2;
        }
        else if(objet.tag == manager.bananeG.tag)
        {
            return 1;
        }
        else if(objet.tag == manager.eauG.tag)
        {
            return 2;
        }
        else if(objet.tag == manager.jusG.tag)
        {
            return 2;
        }
        else if(objet.tag == manager.citronG.tag)
        {
            return 1;
        }
        else if(objet.tag == manager.laitG.tag)
        {
            return 2;
        }
        else if(objet.tag == manager.orangeG.tag)
        {
            return 1;
        }
        else if(objet.tag == manager.pqG.tag)
        {
            return 3;
        }
        else if(objet.tag == manager.yogurtG.tag)
        {
            return 3;
        }
        else if(objet.tag == manager.fromageG.tag)
        {
            return 2;
        }
        else if(objet.tag == manager.marmeladeG.tag)
        {
            return 3;
        }
        else if(objet.tag == manager.pizzaG.tag)
        {
            
            return 3;
        }
        else 
        {
           
            return 2;
        }
        
        
    }
    
}
