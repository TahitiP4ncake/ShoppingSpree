using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Collider C_car;
    private Rigidbody Car;
    private AudioSource A_car;
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.

    
    public float Speed;
    public float Boost;
    public float TurnSpeed;
    public float JumpH;
    public float startingPitch;
    public float Gravity;
    public float Frein;
    public float Flip;
    public float tolerance=100;
    public bool sol;
    private bool BUMP;

    private GamepadManager manager;
    public SoundManager A_manager;
    private float distToGround;
    private float decalage;
    private float angle;
    private Vector3 direction;
    private bool auSol;
    private float pitch;
    public ParticleSystem   Smoke;
    public ParticleSystem   Turbo;
    public Collider boost;
    private bool boostOn;
    public x360_Gamepad gamepad;

    private bool enArriere;

    private float flipAmount;
    private float flipTampon;

    private void Awake()
    {
        Car = GetComponent<Rigidbody>();
        

    }
    void Start()
    {
       //A_manager.Source.Add(gameObject.GetComponent<AudioSource>());
       
        //A_car = GetComponent<AudioSource>();
        
        manager = GamepadManager.Instance;
        gamepad = manager.GetGamepad(1);

        pitch = startingPitch + Random.Range(-0.1f, 0.1f);
        //Smoke.Pause();
        distToGround = C_car.bounds.extents.y+1;
        //Debug.Log(distToGround);
        //Turbo.Pause();
        //bonneOrientation = transform.rotation;
    }
    void Update()
    {
        Debug.Log(Mathf.Atan2(gamepad.GetStick_L().Y, -gamepad.GetStick_L().X) * 180 / Mathf.PI - 90);
        
        IsGrounded();

        
        angle = transform.localEulerAngles.y;
        angle = (angle > 180) ? angle - 360 : angle;
        
/*
        if (auSol && gamepad.GetButton("A") && (gamepad.GetStick_L().Y < 0.5 && gamepad.GetStick_L().Y > -0.5 && gamepad.GetStick_L().X > -0.5 && gamepad.GetStick_L().X < 0.5) && !gamepad.GetButton("B"))
        {

            Move();
            
            if(Turbo.isStopped)
            {
                Turbo.Play();
            }
            
        }
        else
        {
            //Turbo.Stop();
        }
    */
        if ((gamepad.GetStick_L().Y > 0.5 || gamepad.GetStick_L().Y < -0.5 || gamepad.GetStick_L().X < -0.5 || gamepad.GetStick_L().X > 0.5) && auSol)
        {
           
            direction = new Vector3(0,transform.localEulerAngles.y+  Mathf.Atan2(gamepad.GetStick_L().Y, -gamepad.GetStick_L().X) * 180 / Mathf.PI-90, 0);
            if (Mathf.Atan2(gamepad.GetStick_L().Y, -gamepad.GetStick_L().X) * 180 / Mathf.PI - 90 >= -90)
            {
                enArriere = false;
                Move();
            }
            else
            {
                enArriere = true;
                marcheArriere();
            }
            Turn();
            /*
            if (gamepad.GetButton("B") == false)
            {
                Move();
            }
            */
        }

        else
        {
            //Smoke.Stop();
        }

         if (auSol == false)
            {
                Car.velocity = new Vector3(Car.velocity.x, Car.velocity.y - 0.05f, Car.velocity.z);
                Car.AddForce(Car.velocity * 2f);
            
            if (Car.velocity.magnitude < 5 )
            {
               // Car.velocity = new Vector3(Car.velocity.x, Car.velocity.y - 0.05f, Car.velocity.z);
                //Car.AddForce(Car.velocity * 2f);

                Vector3 _good = new Vector3(0, Car.transform.rotation.y, 0);
                Vector3 _bad = new Vector3(Car.transform.rotation.x, Car.transform.rotation.y, Car.transform.rotation.z);



                Vector3 x = Vector3.Cross(_bad.normalized, _good.normalized);
                float theta = Mathf.Asin(x.magnitude);
                Vector3 w = x.normalized * theta / Time.fixedDeltaTime;
                Quaternion q = transform.rotation * Car.inertiaTensorRotation;
                Vector3 T = q * Vector3.Scale(Car.inertiaTensor, (Quaternion.Inverse(q) * w));
                Car.AddTorque(T / 75, ForceMode.VelocityChange);

            }
            
            
          

        }
         /*
        if (gamepad.GetButton("B") && auSol)
        {
            marcheArriere();
        }
        */
       

       

        if (sol)
        {

            if (Car.transform.eulerAngles.x > 80 && Car.transform.eulerAngles.x < 100)
            {
                //Debug.Log("DH ADSHGAU");
                Car.AddRelativeTorque(new Vector3(-20, 0, 0), ForceMode.VelocityChange);

            }
            if (Car.transform.eulerAngles.x >= 180 && Car.transform.eulerAngles.x < 290)
            {

                Car.AddRelativeTorque(new Vector3(20, 0, 0), ForceMode.VelocityChange);
            }
            if (Car.transform.eulerAngles.z > 70 && Car.transform.eulerAngles.z < 180)
            {
                
                Car.AddRelativeTorque(new Vector3(0, 0, -20), ForceMode.VelocityChange);
            }

        }
    }
    
   
    private void Turn()
    {

        float step = TurnSpeed * Time.deltaTime;
       
        Quaternion turnRotation = Quaternion.Euler(0f, direction.y, 0f);
        
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, turnRotation, step);
        

    }
   

    
    private void marcheArriere()
    {
        float _hauteur = Car.velocity.y * Time.deltaTime;
        Vector3 movement = transform.forward * -Speed * Time.deltaTime;
        movement = -movement;

        movement.y = _hauteur;
        if (gamepad.GetButton("A"))
        {
            movement = movement * Boost;
            

        }
       
        if (boostOn == true)
        {
            movement = movement * 3.5f;
        }
       // Car.AddForce(movement, ForceMode.Impulse);
    }
    private void Move()
    {
        float _hauteur = Car.velocity.y*Time.deltaTime;
        Vector3 movement = transform.forward  * -Speed *Time.deltaTime;
       
        movement.y = _hauteur;
        
        if (gamepad.GetButton("A"))
        {
            movement = movement * Boost;
            
        }
       
        if(boostOn==true)
        {
            movement = movement * 3.5f;
        }

      
       // Car.AddForce(movement, ForceMode.Impulse);


    }

  
    
     void OnTriggerEnter(Collider boost)
    {
       
    }
    void OnTriggerExit(Collider boost)
    {
       

    }
    void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.tag == "LD" && auSol == false)
        {
            sol = true;
            Debug.Log("OUIIIIIII");
        }
        else
        {
            sol = false;
        }
    }
    void OnCollisionStay(Collision ground)
    {
        if(ground.gameObject.tag=="LD"&& auSol==false)
        {
            sol = true;
            Debug.Log("OUIIIIIII");
        }
        else
        {
            sol = false;
        }
    }
    
    
    private void IsGrounded()
    {
        
        if(Physics.Raycast(transform.position, -transform.up, distToGround+tolerance))
        {


            auSol = true;
            return;
        }
        else
        {
            auSol = false;
        }
    }
    /*
    private void BarrelRoll()
    {
       
        
        if (auSol)
        {
            Car.AddForce(transform.up * 1.5f, ForceMode.VelocityChange);

            //StartCoroutine(getDown());
        }
        Car.AddRelativeTorque(new Vector3(1, 0, 0) ,ForceMode.VelocityChange );
        
    }
    private void Jump()
    {
        if(auSol)
        {
            
            Car.AddForce(transform.up*1.5f, ForceMode.VelocityChange);
            
            //StartCoroutine(getDown());
        }
       
        
            Car.AddRelativeTorque(new Vector3(0, 0, 30), ForceMode.VelocityChange);
        BUMP = false;
    }*/
    /*
    IEnumerator getDown()
    {
        yield return new WaitForSecondsRealtime(1.2f);
        Car.AddRelativeForce(new Vector3(0,-2,0),ForceMode.Impulse) ;
        
    }
    */
/*
IEnumerator inactif()
    {
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("1");
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("2");
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("3");
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("deconnecte toi");
    }
   */
    
    
}






 
     