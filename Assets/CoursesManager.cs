using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoursesManager : MonoBehaviour
{

    public List<GameObject> Courses = new List<GameObject>();
    public List<GameObject> Objets = new List<GameObject>();
    public List<GameObject> CoursesTemp = new List<GameObject>();

    public int nombreCourses;
    private int nombreTemp;


    //liste des objets pour la manière chiante
    public int itemMax;
    public int carottesN;
    public int carottesO;
    public GameObject carottesG;
    public int tomatesN;
    public int tomatesO;
    public GameObject tomatesG;
    public int sodaN;
    public int sodaO;
    public GameObject sodaG;
    public int ananasN;
    public int ananasO;
    public GameObject ananasG;
    public int cassouletO;
    public int cassouletN;
    public GameObject cassouletG;
    public int poisN;
    public int poisO;
    public GameObject poisG;

    public Text liste;
    public Text Warning;

    public AnimationCurve Spawn;
    private float textAnima;

    public ObjetPickUp papaPickUp;

    public AnimationCurve exitY;
    public AnimationCurve exitSize;
    private float exitT;
    private float origin;
    void Start()
    {
        origin = Warning.transform.position.y;
        liste.text = "";
        Warning.text = "";

        //on choisit

        for (int i = 0; i < nombreCourses;i++)
        {

            int _choice = Random.Range(1, itemMax+1);
            if(_choice ==1)
            {
                carottesN += 1;
            }
            if (_choice ==2)
            {
                tomatesN += 1;
            }
            if (_choice ==3)
            {
                sodaN += 1;
            }
            if (_choice ==4)
            {
                ananasN += 1;
            }
            if (_choice ==5)
            {
                cassouletN += 1;
            }
            if (_choice ==6)
            {
                poisN += 1;
            }
        }
        // on applique

        UpdateListe();
    }

    public void UpdateListe()
    {
        liste.text = "";

        liste.text += (carottesN == 0) ? "" : (carottesN == 1) ? (carottesO + " / " + carottesN + " carrot\n") : (carottesO + " / " + carottesN + " carrots\n");
                        

        liste.text += (tomatesN == 0) ? "" : (tomatesN == 1) ? (tomatesO + " / " + tomatesN + " tomato\n") : (tomatesO + " / " + tomatesN + " tomatoes\n");

        liste.text += (sodaN == 0) ? "" : (sodaN == 1) ? (sodaO + " / " + sodaN + " soda\n") : (sodaO + " / " + sodaN + " sodas\n");

        liste.text += (ananasN == 0) ? "" : (ananasN == 1) ? (ananasO + " / " + ananasN + " pineapple\n") : (ananasO + " / " + ananasN + " pineapples\n");

        liste.text += (cassouletN == 0) ? "" : (cassouletN == 1) ? (cassouletO + " / " + cassouletN + " cassoulet\n") : (cassouletO + " / " + cassouletN + " cassoulets\n");

        liste.text += (poisN == 0) ? "" : (poisN == 1) ? (poisO + " / " + poisN + " pea\n") : (poisO + " / " + poisN + " peas\n");

        CheckWin();
    }

    void CheckWin()
    {
        
        if (carottesO>=carottesN && tomatesO>=tomatesN && sodaO>=sodaN && ananasO>=ananasN && cassouletO>=cassouletN && poisO>=poisN)
        {


            Warning.text = "RUN TO THE EXIT";
            textAnima = .001f;
            papaPickUp.WinScreen();
            exitT = 0.001f;


        }
    }

    void Update()
    {

        if (textAnima > 0)
            {
                Warning.transform.localScale = new Vector3(Spawn.Evaluate(textAnima), Spawn.Evaluate(textAnima), 1);
                textAnima += Time.deltaTime;
                if (Spawn.Evaluate(textAnima) <= 0)
                {
                    textAnima = 0;
                    //W.gameObject.SetActive(false);
                }
            }
        if (exitT > 0)
        {
            Warning.transform.position = new Vector3(Warning.transform.position.x, origin + exitY.Evaluate(exitT), Warning.transform.position.z);
            Warning.transform.localScale = new Vector3(exitSize.Evaluate(exitT), exitSize.Evaluate(exitT), exitSize.Evaluate(exitT));
            exitT += Time.deltaTime;
            if (exitY.Evaluate(exitT) <= 0)
            {
                exitT = 0.001f;
                //W.gameObject.SetActive(false);
            }
        }
        //Warning.transform.localScale = new Vector3(Spawn.Evaluate(Time.time), Spawn.Evaluate(Time.time), Spawn.Evaluate(Time.time));
    }

    GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == 8)
            {
                Objets.Add(goArray[i]);
            }
        }
        if (Objets.Count == 0)
        {
            return null;
        }
        return Objets.ToArray();
    }

    public void ObjectOnTheList(GameObject _object)
    {
        foreach (GameObject Item in Courses)
        {
            if (Item == _object)
            {
                print("trouvé");
                Courses.Remove(Item);
                //break;
            }

        }
    }

}


/*
 
    Du coup,
    il me faut le random pick du début de partie : on définit quels objets le joueur va devoir récupérer pour pouvoir gagner.
    il faut une liste avec tout les objets disponibles à partir de laquelle je créer la liste de courses.
    il me faut un truc qui detecte quel objet je récupère pour savoir si je remplis la liste ou si j'achete des trucs en trop.
    et il faut un prix à chaque objet (bonus win screen)

    il va falloir faire les bools uniques



*/
