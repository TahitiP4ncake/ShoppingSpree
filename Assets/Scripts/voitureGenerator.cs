using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voitureGenerator : MonoBehaviour {
    public GameObject voiture;
    public GameObject muscle;
    public List<GameObject> Voitures = new List<GameObject>();
    private int nombreDeVoiture;
    private int nombreDeGamepad;
    private int numero;

    public bool joueur1;
    public bool joueur2;
    public bool joueur3;
    public bool joueur4;

    private int createur;
    private GameObject modele;
    //public bool joueur1;
    //public bool joueur2;

    public x360_Gamepad gamepad1;
    public x360_Gamepad gamepad2;
    public x360_Gamepad gamepad3;
    public x360_Gamepad gamepad4;


    private GamepadManager manager;
    // Use this for initialization
    
	void Start () {
        manager = GamepadManager.Instance;
        gamepad1 = manager.GetGamepad(1);
        gamepad2 = manager.GetGamepad(2);
        gamepad3 = manager.GetGamepad(3);
        gamepad4 = manager.GetGamepad(4);
    }
	
	// Update is called once per frame
	void Update () {

        nombreDeGamepad = manager.ConnectedTotal();
        /* if(manager.GetButtonDownAny("R3")&& nombreDeVoiture<nombreDeGamepad)
         {

             CreateCar();
         }
         */
        if (nombreDeGamepad >= 1)
        {
            if (gamepad1.GetButtonDown("R3")&& joueur1==false)
            {
                createur = 1;
                modele = voiture;
                CreateCar(createur, modele);
                joueur1 = true;
            }
        }
        if (nombreDeGamepad >= 2)
        {
            if (gamepad2.GetButtonDown("R3")&&joueur2 == false)
            {
                createur = 2;
                modele = muscle;
                CreateCar(createur, modele);
                joueur2 = true;
            }
        }
        if (nombreDeGamepad >= 3)
        {
            if (gamepad3.GetButtonDown("R3") && joueur3 == false)
            {
                createur = 3;
                modele = voiture;
                CreateCar(createur, modele);
                joueur3 = true;
            }
        }
        if (nombreDeGamepad >= 4)
        {
            if (gamepad4.GetButtonDown("R3") && joueur4 == false)
            {
                createur = 4;
                modele = muscle;
                CreateCar(createur, modele);
                joueur4 = true;
            }
        }
        //Debug.Log(Voitures.Count);
    }

    GameObject CreateCar(int createur, GameObject modele)
    {
        GameObject _voiture;
        _voiture = Instantiate(modele, transform.position, transform.rotation) as GameObject;
        nombreDeVoiture += 1;
        //_voiture.GetComponent<CarController>().papa = createur;


        _voiture.tag = "Player";
        //_controller = manager.GetGamepad(nombreDeVoiture);
        //Voitures.Add(_voiture);
        return _voiture;
    }

   /* void association()
    {
        for (int y = 1; y < nombreDeGamepad; y++)
        {
            gamepad1 = manager.GetGamepad(y);
        }
    }
    */
}
/* 
 j'ai besoin de compter le nombre de voiture active et le nombre de controller connectés 
 j'update à chaque frame ?
 */