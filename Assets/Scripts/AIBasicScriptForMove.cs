using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBasicScriptForMove : MonoBehaviour
{

    public Transform player;
    public List<GameObject> DestinationPoints;
    public float speed;
    public float alertDistance;
    public float walkingDistance;
    public float attackingDistance;
    public float remainingDistance;
    public int minTime;
    public int maxTime;

    private Animator anim;
    private Vector3 direction;
    private NavMeshAgent agent;
    private int selectedDestination;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.enabled == true && agent.remainingDistance < remainingDistance)
        {
            agent.enabled = false;

            //tu mettras le bon nom d'anim 
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                anim.SetTrigger("reboot");
                
            }
            

            StartCoroutine(RandomMovement());

            Debug.Log("Arrived");
        }
        /*
        //Alert
        if (Vector3.Distance(player.position, transform.position) < alertDistance && Vector3.Distance(player.position, transform.position) > attackingDistance)
        {
            
            agent.enabled = false;

            //tu mettras le bon nom d'anim 
            
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("blow"))
            {
                anim.SetTrigger("blow");

            }
            
        }
    */
        //Attacking
        else if (Vector3.Distance(player.position, transform.position) <= walkingDistance)
        {
            agent.enabled = true;

            

            agent.SetDestination(player.transform.position);

            //tu mettras le bon nom d'anim 
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walking"))
            {
                anim.SetTrigger("walk");

            }

            if (direction.magnitude <= attackingDistance)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("run") || !anim.GetCurrentAnimatorStateInfo(0).IsName("blow"))
                {
                    anim.SetTrigger("blow");

                }
            }
        }

        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && agent.enabled == false)
        {
            //tu mettras le bon nom d'anim 
            
                anim.SetTrigger("reboot");

            

            StartCoroutine(RandomMovement());
        }
        //Idle
    }

    public IEnumerator RandomMovement()
    {
        int index = Random.Range(minTime, maxTime);

        Debug.Log("RandomTime: " + index);

        yield return new WaitForSeconds(index);

        int index2 = Random.Range(1, 3);

        switch (index2)
        {

            case 1:
                //garde l'idle et appelle RandomMovement encore
                Debug.Log("keepsIdle");
                StartCoroutine(RandomMovement());
                break;

            case 2:
                //garde une randomDestination & move 
                Debug.Log("Move...");
                agent.enabled = true;
                int lastDestination = selectedDestination;
                selectedDestination = Random.Range(0, DestinationPoints.Count);

                //ça c'est pour quand ya plusieurs gardes qui bougent 
                while (selectedDestination == lastDestination || DestinationPoints[selectedDestination].GetComponent<DestinationPointsScript>().isUsed == true)
                {
                    selectedDestination = Random.Range(0, DestinationPoints.Count);
                    break;
                }

                //ici ça veut pas je sais pas pourquoi :'(
                DestinationPoints[lastDestination].GetComponent<DestinationPointsScript>().isUsed = false;
                selectedDestination = Random.Range(0, DestinationPoints.Count);
                DestinationPoints[selectedDestination].GetComponent<DestinationPointsScript>().isUsed = true;

                agent.SetDestination(DestinationPoints[selectedDestination].transform.position);

                //tu mettras le bon nom d'anim 
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walking"))
                {
                    anim.SetTrigger("walk");

                }

                break;

        }
    }

}

/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBasicScriptForMove : MonoBehaviour
{

    public Transform player;
    public List<GameObject> DestinationPoints;
    public float speed;
    public float alertDistance;
    public float walkingDistance;
    public float attackingDistance;
    public float remainingDistance;
    public int minTime;
    public int maxTime;

    private Animator anim;
    private Vector3 direction;
    private NavMeshAgent agent;
    private int selectedDestination;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.enabled == true && agent.remainingDistance < remainingDistance)
        {
            agent.enabled = false;

            //tu mettras le bon nom d'anim 
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);

            StartCoroutine(RandomMovement());

            Debug.Log("Arrived");
        }

        //Alert
        if (Vector3.Distance(player.position, transform.position) < alertDistance && Vector3.Distance(player.position, transform.position) > attackingDistance)
        {
            
            agent.enabled = false;

            //tu mettras le bon nom d'anim 
            anim.SetBool("isAlert", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", false);
        }
        //Attacking
        else if (Vector3.Distance(player.position, transform.position) <= walkingDistance)
        {
            agent.enabled = true;

            

agent.SetDestination(player.transform.position);

            //tu mettras le bon nom d'anim 
            anim.SetBool("isAlert", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", true);

            if (direction.magnitude <= attackingDistance)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }

        else if (anim.GetBool("isIdle") == false && agent.enabled == false)
        {
            //tu mettras le bon nom d'anim 
            anim.SetBool("isAlert", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", false);

            StartCoroutine(RandomMovement());
        }
        //Idle
    }

    public IEnumerator RandomMovement()
{
    int index = Random.Range(minTime, maxTime);

    Debug.Log("RandomTime: " + index);

    yield return new WaitForSeconds(index);

    int index2 = Random.Range(1, 3);

    switch (index2)
    {

        case 1:
            //garde l'idle et appelle RandomMovement encore
            Debug.Log("keepsIdle");
            StartCoroutine(RandomMovement());
            break;

        case 2:
            //garde une randomDestination & move 
            Debug.Log("Move...");
            agent.enabled = true;
            int lastDestination = selectedDestination;
            selectedDestination = Random.Range(0, DestinationPoints.Count);

            //ça c'est pour quand ya plusieurs gardes qui bougent 
            while (selectedDestination == lastDestination || DestinationPoints[selectedDestination].GetComponent<DestinationPointsScript>().isUsed == true)
            {
                selectedDestination = Random.Range(0, DestinationPoints.Count);
                break;
            }

            //ici ça veut pas je sais pas pourquoi :'(
            DestinationPoints[lastDestination].GetComponent<DestinationPointsScript>().isUsed = false;
            selectedDestination = Random.Range(0, DestinationPoints.Count);
            DestinationPoints[selectedDestination].GetComponent<DestinationPointsScript>().isUsed = true;

            agent.SetDestination(DestinationPoints[selectedDestination].transform.position);

            //tu mettras le bon nom d'anim 
            anim.SetBool("isAlert", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", true);

            break;

    }
}

}

*/