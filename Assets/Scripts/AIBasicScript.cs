using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicScript : MonoBehaviour
{

    public Transform player;
    public float speed;
    public float alertDistance;
    public float walkingDistance;
    public float attackingDistance;


    public Animator anim;
    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
       // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Alert
        if (Vector3.Distance(player.position, transform.position) < alertDistance && Vector3.Distance(player.position, transform.position) > attackingDistance)
        {
            //tu mettras le bon nom d'anim 
            anim.SetBool("isAlert", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", false);
        }
        //Attacking
        else if (Vector3.Distance(player.position, transform.position) <= walkingDistance)
        {
            direction = player.position - transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.9f * Time.deltaTime);

            transform.Translate(0, 0, speed);

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

        else if (anim.GetBool("isIdle") == false)
        {
            //tu mettras le bon nom d'anim 
            anim.SetBool("isAlert", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", false);
        }
        //Idle

    }
}
