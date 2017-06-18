using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gardeIA : MonoBehaviour
{

    public Transform gardeTransform;
    public GameObject papa;

    public float CD=2;
    public float distanceMax;

    bool following;
    bool lost;
    bool standing;
    bool detected;
    bool perdu;

    bool lose;
    RaycastHit hit;

    public float speed;

    public NavMeshAgent garde;
    public Animator anim;

    public CaddieController papaC;
    public gameOver over;

    public winState win;

    void Start()
    {
        garde = GetComponent<NavMeshAgent>();
        garde.enabled = true;
    }

    void Stop()
    {
        garde.enabled = false;
        garde.speed = 0;
        //gameObject.transform.rotation = Quaternion.Euler(papa.transform.position - transform.position);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (!lose)
        {
            lose = true;
            anim.SetTrigger("lose");
        }
    }

    void Update()
    {
        if (!win.winOn )
        {
            if (!papaC.isDead)
            {
                CheckState();
            }
            else
            {
                Stop();
            }
        }
        else
        {
            Stop();
            
        }
        //print(Vector3.Distance(transform.position, papa.transform.position));
    }
    void CheckState()
    {
       


        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y +1,transform.position.z), papa.transform.position - transform.position, out hit) && hit.transform.tag == "Player")
        {
            if (Vector3.Distance(transform.position, papa.transform.position) < distanceMax)
            {
                if (!perdu)
                {
                    perdu = true;
                    over.gameOverOn = true;
                    papaC.isDead = true;
                    papaC.garde = true;
                    garde.enabled = false;
                    garde.speed = 0;
                    //gameObject.transform.rotation = Quaternion.Euler(papa.transform.position - transform.position);
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    //print("gotcha!");
                    anim.SetTrigger("lose");
                }
                if(perdu)
                {
                    Vector3 targetDir = papa.transform.position - transform.position;
                    float step = speed * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                    //Debug.DrawRay(transform.position, newDir, Color.red);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }

            }
            else
            {


                Debug.DrawRay(transform.position, papa.transform.position - transform.position, Color.green);
                //print("je te vois");
                if (!following)
                {
                    standing = false;
                    following = true;
                    lost = false;
                    StopAllCoroutines();
                    StartCoroutine(detection());
                    //suivre
                }
                else
                {
                    if (detected)
                    {
                        StopAllCoroutines();
                        garde.SetDestination(papa.transform.position);
                        //print("je te suis");
                        lost = false;
                    }

                }
                //si il le suit et qu'il est toujours en vue il ne fera rien de plus que le suivre
            }
        }
        else if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), papa.transform.position - transform.position, out hit) && hit.transform.tag != "Player")
        {
            
            Debug.DrawRay(transform.position, papa.transform.position - transform.position, Color.red);
            if (following)
            {
                if (!lost)
                {
                    lost = true;
                    StartCoroutine(tracking());

                }
                garde.SetDestination(papa.transform.position);
                //continuer à suivre pendant un petit moment
            }
            if(!following && Vector3.Distance(transform.position,gardeTransform.position)>2)
            {
                //retour au poste
                garde.SetDestination(gardeTransform.position);
                //print("je rentre à la maison");
                detected = false;
            }
            else if(!following && Vector3.Distance(transform.position, gardeTransform.position) < 2)
            {
                if(!standing)
                {
                    //print("je reste ou je suis");
                    standing = true;
                    anim.SetTrigger("reboot");
                }
            }
        }
    }

    IEnumerator tracking()
    {
        //running back home
        //print("tracking");
        yield return new WaitForSecondsRealtime(CD);


        following = false;
        if (!win.winOn)
        {
            if (!papaC.isDead)
            {
                garde.SetDestination(gardeTransform.position);
            }
        }
    }

    IEnumerator detection()
    {
        //print("detected");
        
        //gameObject.transform.rotation = Quaternion.Euler(papa.transform.position - transform.position); 
        anim.SetTrigger("blow");
        garde.isStopped = true;
       yield return new WaitForSecondsRealtime(1);
        //print("je cours");
        if (!win.winOn)
        {
            if (!papaC.isDead)
            {
                garde.SetDestination(papa.transform.position);
            }
        }
        garde.isStopped = false;
        detected = true;

    }
}
