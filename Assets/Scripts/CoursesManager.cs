using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoursesManager : MonoBehaviour
{

    public List<GameObject> Courses = new List<GameObject>();
    public List<GameObject> Objets = new List<GameObject>();
    public List<Rayon> Rayons = new List<Rayon>();

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
    public int aubergineN;
    public int aubergineO;
    public GameObject aubergineG;
    public int baguetteN;
    public int baguetteO;
    public GameObject baguetteG;
    public int bananeN;
    public int bananeO;
    public GameObject bananeG;
    public int eauN;
    public int eauO;
    public GameObject eauG;
    public int jusN;
    public int jusO;
    public GameObject jusG;
    public int citronN;
    public int citronO;
    public GameObject citronG;
    public int laitN;
    public int laitO;
    public GameObject laitG;
    public int orangeN;
    public int orangeO;
    public GameObject orangeG;
    public int pqN;
    public int pqO;
    public GameObject pqG;
    public int yogurtN;
    public int yogurtO;
    public GameObject yogurtG;
    public int fromageN;
    public int fromageO;
    public GameObject fromageG;
    public int marmeladeN;
    public int marmeladeO;
    public GameObject marmeladeG;
    public int pizzaN;
    public int pizzaO;
    public GameObject pizzaG;
    public int eggN;
    public int eggO;
    public GameObject eggG;


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
                if (carottesN == 0)
                {
                    nombreTemp += 1;
                }
                carottesN += 1;
                Courses.Add(carottesG);
                
            }
            if (_choice ==2)
            {
                if (tomatesN == 0)
                {
                    nombreTemp += 1;
                }
                tomatesN += 1;
                Courses.Add(tomatesG);
            }
            if (_choice ==3)
            {
                if (sodaN == 0)
                {
                    nombreTemp += 1;
                }
                sodaN += 1;
                Courses.Add(sodaG);
            }
            if (_choice ==4)
            {
                if (ananasN == 0)
                {
                    nombreTemp += 1;
                }
                ananasN += 1;
                Courses.Add(ananasG);
            }
            if (_choice ==5)
            {
                if (cassouletN == 0)
                {
                    nombreTemp += 1;
                }
                cassouletN += 1;
                Courses.Add(cassouletG);
            }
            if (_choice ==6)
            {
                if (poisN == 0)
                {
                    nombreTemp += 1;
                }
                poisN += 1;
                Courses.Add(poisG);
            }
            if (_choice == 7)
            {
                if (aubergineN == 0)
                {
                    nombreTemp += 1;
                }
                aubergineN += 1;
                Courses.Add(aubergineG);
            }
            if (_choice == 8)
            {
                if (baguetteN == 0)
                {
                    nombreTemp += 1;
                }
                baguetteN += 1;
                
            }
            if (_choice == 9)
            {
                if (bananeN == 0)
                {
                    nombreTemp += 1;
                }
                bananeN += 1;

            }
            if (_choice == 10)
            {
                if (eauN == 0)
                {
                    nombreTemp += 1;
                }
                eauN += 1;

            }
            if (_choice == 11)
            {
                if (jusN == 0)
                {
                    nombreTemp += 1;
                }
                jusN += 1;

            }
            if (_choice == 12)
            {
                if (citronN == 0)
                {
                    nombreTemp += 1;
                }
                citronN += 1;

            }
            if (_choice == 13)
            {
                if (laitN == 0)
                {
                    nombreTemp += 1;
                }
                laitN += 1;

            }
            if (_choice == 14)
            {
                if (orangeN == 0)
                {
                    nombreTemp += 1;
                }
                orangeN += 1;

            }
            if (_choice == 15)
            {
                if (pqN == 0)
                {
                    nombreTemp += 1;
                }
                pqN += 1;

            }
            if (_choice == 16)
            {
                if (yogurtN == 0)
                {
                    nombreTemp += 1;
                }
                yogurtN += 1;

            }
            if (_choice == 17)
            {
                if (fromageN == 0)
                {
                    nombreTemp += 1;
                }
                fromageN += 1;

            }
            if (_choice == 18)
            {
                if (marmeladeN == 0)
                {
                    nombreTemp += 1;
                }
                marmeladeN += 1;

            }
            if (_choice == 19)
            {
                if (pizzaN == 0)
                {
                    nombreTemp += 1;
                }
                pizzaN += 1;

            }
            if (_choice == 20)
            {
                if (eggN == 0)
                {
                    nombreTemp += 1;
                }
                eggN += 1;

            }
            if (nombreTemp == 4)
            {
                break;
            }
        }
        /*
        GameObject[] gos = (GameObject.FindGameObjectsWithTag("Rayon"));
        foreach (GameObject _rayon in gos)
        {
            //print("coucou2");
            Rayon _rayonS = _rayon.GetComponent<Rayon>();
            //GameObject _rayonO = _rayonS.objet;
            foreach (GameObject course in Courses)
            {
                if (_rayonS.objet.tag == course.tag)
                {
                    print("coucou");
                    _rayon.GetComponent<Rayon>().pointerOn();
                    _rayonS.objet.tag == carottesG.tag || _rayonS.objet.tag == tomatesG.tag ||
                _rayonS.objet.tag == sodaG.tag || _rayonS.objet.tag == ananasG.tag ||
                _rayonS.objet.tag == cassouletG.tag || _rayonS.objet.tag == poisG.tag
                

                }


            }
            

        }
        */
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

        liste.text += (aubergineN == 0) ? "" : (aubergineN == 1) ? (aubergineO + " / " + aubergineN + " eggplant\n") : (aubergineO + " / " + aubergineN + " eggplants\n");

        liste.text += (baguetteN == 0) ? "" : (baguetteN == 1) ? (baguetteO + " / " + baguetteN + " baguette\n") : (baguetteO + " / " + baguetteN + " baguettes\n");

        liste.text += (bananeN == 0) ? "" : (bananeN == 1) ? (bananeO + " / " + bananeN + " banana\n") : (bananeO + " / " + bananeN + " bananas\n");

        liste.text += (eauN == 0) ? "" : (eauN == 1) ? (eauO + " / " + eauN + " bottle\n") : (eauO + " / " + eauN + " bottles\n");

        liste.text += (jusN == 0) ? "" : (jusN == 1) ? (jusO + " / " + jusN + " juice\n") : (jusO + " / " + jusN + " juices\n");

        liste.text += (citronN == 0) ? "" : (citronN == 1) ? (citronO + " / " + citronN + " lemon\n") : (citronO + " / " + citronN + " lemons\n");

        liste.text += (laitN == 0) ? "" : (laitN == 1) ? (laitO + " / " + laitN + " milk\n") : (laitO + " / " + laitN + " milks\n");

        liste.text += (orangeN == 0) ? "" : (orangeN == 1) ? (orangeO + " / " + orangeN + " orange\n") : (orangeO + " / " + orangeN + " oranges\n");

        liste.text += (pqN == 0) ? "" : (pqN == 1) ? (pqO + " / " + pqN + " toilet Paper\n") : (pqO + " / " + pqN + " toilet Papers\n");

        liste.text += (yogurtN == 0) ? "" : (yogurtN == 1) ? (yogurtO + " / " + yogurtN + " yogurt\n") : (yogurtO + " / " + yogurtN + " yogurts\n");

        liste.text += (fromageN == 0) ? "" : (fromageN == 1) ? (fromageO + " / " + fromageN + " cheese\n") : (fromageO + " / " + fromageN + " cheeses\n");

        liste.text += (marmeladeN == 0) ? "" : (marmeladeN == 1) ? (marmeladeO + " / " + marmeladeN + " marmelade\n") : (marmeladeO + " / " + marmeladeN + " marmelades\n");

        liste.text += (pizzaN == 0) ? "" : (pizzaN == 1) ? (pizzaO + " / " + pizzaN + " pizza\n") : (pizzaO + " / " + pizzaN + " pizzas\n");

        liste.text += (eggN == 0) ? "" : (eggN == 1) ? (eggO + " / " + eggN + " egg\n") : (eggO + " / " + eggN + " eggs\n");


        CheckWin();
    }

    void CheckWin()
    {
        
        if (carottesO>=carottesN 
            && tomatesO>=tomatesN 
            && sodaO>=sodaN 
            && ananasO>=ananasN 
            && cassouletO>=cassouletN 
            && poisO>=poisN 
            && aubergineO >= aubergineN 
            && baguetteO >= baguetteN 
            && bananeO >= bananeN 
            && eauO >= eauN 
            && jusO >= jusN 
            && citronO >= citronN 
            && laitO >= laitN 
            && orangeO >= orangeN 
            && pqO >= pqN 
            && yogurtO >= yogurtN
            && fromageO >= fromageN
            && marmeladeO >= marmeladeN
            && pizzaO >= pizzaN
            && eggO >= eggN)
        {


            Warning.text = "RUN TO THE EXIT";
            textAnima = .001f;
            papaPickUp.winning = true;
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

    void ActivatePointer()
    {
        foreach(GameObject objet in Courses)
        {
            //objet.tag == 
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
