using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAncien : MonoBehaviour {

    public Transform papaTransform;
    public Transform[] fearPoints;
    public Transform respawnPoint;

    public float delay;

    public Animator cop;

    bool attackMode = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time > delay && GetComponent<UnityEngine.AI.NavMeshAgent>().enabled)
        {
            if (attackMode == true)
            {
                
                    cop.SetTrigger("blow");
                
                GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(papaTransform.position);
            }
            else
            {
                //fuite
               

            if(papaTransform.position.x > 0 && papaTransform.position.z < 0 )
                {
                    GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(fearPoints[0].position);
                }
            else if (papaTransform.position.x < 0 && papaTransform.position.z < 0)
                {
                    GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(fearPoints[1].position);
                }
            else if (papaTransform.position.x < 0 && papaTransform.position.z > 0)
                {
                    GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(fearPoints[3].position);
                }
            else
                {
                    GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(fearPoints[2].position);
                }
            }
        }
	}

    public void Stop()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
    }

    public void SetDefensiveMode()
    {
        attackMode = false;
        Invoke("SetAttackMode", 15.0f);
    }

    void SetAttackMode()
    {
        attackMode = true;
    }

    public bool IsAttackMode()
    {
        return attackMode;
    }

    public void Respawn()
    {
        //on désactive le pathfinding
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.enabled = false;

        //on positionne le garde au point de respawn
        GetComponent<Transform>().position = respawnPoint.position;

        //on réactive le pathfinding
        agent.enabled = true;

        //on annule la plannification de l'appel de "setAttackMode"
        CancelInvoke("SetAttackMode");

        //on repasse tout de suite en mode attaque
        SetAttackMode();
    }
}
