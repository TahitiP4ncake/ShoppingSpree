using System.Collections;
using UnityEngine;


public class CaddieController1 : MonoBehaviour {
    private GamepadManager manager;
    public x360_Gamepad gamepad;

    private Rigidbody rb;
    

    public float Speed;
    public float TurnSpeed;
    public float boostSpeed;
    public float bigBoostSpeed;
    public float bigBoostDuration;
    public float boostRecovery;
    public float JumpHigh;

    private Vector3 direction;
    private float angle;
    private float arriere;

    private bool bigBoost;
    private bool bigBoostActive;
    private bool boost;
    private bool jumpRecovery;
    public float actualSpeed;

    //ines
    bool isDead = false;

    Vector3 respawnPoint;

    void Start () {
        rb = GetComponent<Rigidbody>();
        manager = GamepadManager.Instance;
        gamepad = manager.GetGamepad(1);
        //ines
       // respawnPoint = GetComponent<Transform>().position;
    }

    void Update()
    {
        //ines
        if (isDead == false)
        {
            actualSpeed = rb.velocity.magnitude;
            if ((gamepad.GetStick_L().Y > 0.5 || gamepad.GetStick_L().Y < -0.5 || gamepad.GetStick_L().X < -0.5 || gamepad.GetStick_L().X > 0.5))
            {
                angle = Mathf.Atan2(gamepad.GetStick_L().Y, -gamepad.GetStick_L().X) * 180 / Mathf.PI - 90;
                direction = new Vector3(0, transform.localEulerAngles.y + angle, 0);

                if (angle > -45 && angle < 45)
                {

                    arriere = gamepad.GetStick_L().Y;
                    Turn();
                    Move();
                }
                if (angle < -175 && angle > -185)
                {
                    arriere = gamepad.GetStick_L().Y;
                    Move();
                }
                if ((angle <= -45 && angle >= -175) || (angle >= 45 || angle <= -185))
                {
                    if (bigBoost && arriere > 0)
                    {
                        arriere = 1;
                        Move();
                    }
                    else
                    {
                        arriere = gamepad.GetStick_L().Y;
                        Turn();
                        Move();
                    }
                }

            }

            //boost puis vitesse augmentée
            if (gamepad.GetButtonDown("A") && !bigBoostActive)
            {
                StartCoroutine(Turbo());
            }
            if (gamepad.GetButton("A"))
            {
                boost = true;
            }
            if (!gamepad.GetButton("A"))
            {
                boost = false;

            }
            if (gamepad.GetButtonDown("X") && !jumpRecovery)
            {
                StartCoroutine(jumpTime());
                Jump();
            }
        }
    }

    IEnumerator Turbo()
    {
        bigBoost = true;
        bigBoostActive = true;
        yield return new WaitForSecondsRealtime(bigBoostDuration);
        bigBoost = false;
        yield return new WaitForSecondsRealtime(boostRecovery);
        bigBoostActive = false;
    }
    IEnumerator jumpTime()
    {
        jumpRecovery = true;
        yield return new WaitForSecondsRealtime(1f);
        jumpRecovery = false;
    }
/*
    //ines
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "garde")
        {
            //on récupère le script
            AI gardeAI = col.gameObject.GetComponent<AI>();

            if(gardeAI.IsAttackMode() == true)
            {
                isDead = true;
               GameObject[] gardes = GameObject.FindGameObjectsWithTag("garde");

                //on stop les gardes
                foreach (GameObject garde in gardes)
                {
                    garde.GetComponent<AI>().Stop(); 
                }
            }
        }
    }
    */
    void Move()
    {
        
        Vector3 movement = transform.forward *-Speed *arriere;
        if (bigBoost)
        {
            movement = movement * bigBoostSpeed;
            rb.AddForce(movement, ForceMode.VelocityChange);
        }
        else
        {
            if (boost)
            {
                movement = movement * boostSpeed;
            }
            rb.AddForce(movement, ForceMode.Force);
        }
    }
    void Turn()
    {
        float step = TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, direction.y, 0f);


        transform.rotation = Quaternion.RotateTowards(transform.rotation, turnRotation, step);
    }

    void Jump()
    {
        rb.AddForce(transform.up * JumpHigh, ForceMode.VelocityChange);

    }
    
}




