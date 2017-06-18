using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaddieController : MonoBehaviour {
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
    public bool isPause;

    private Vector3 direction;
    private float angle;
    private float arriere;

    private bool bigBoost;
    private bool bigBoostActive;
    private bool boost;
    private bool jumpRecovery;
    public float actualSpeed;
    private bool restart;
    public bool isDead = false;
    Vector3 respawnPoint;

    public Animator anim;
    public ParticleSystem dust;
    public ParticleSystem sparkA;
    public ParticleSystem sparkB;

    public Image screen;
    public Text pauseText;
    public AnimationCurve pauseMove;
    private float textAnima;
    private Vector3 textOriginP;

    public Text gameOverText;
    public Text pressA;

    public bool isWin= false;

    public Text listeT;
    public Image listeI;
    public Text timerT;
    public Image timerI;
    public timer timer;
    public Text run;

    public bool garde;

    void Start () {
        textOriginP = pauseText.transform.position;
        rb = GetComponent<Rigidbody>();
        manager = GamepadManager.Instance;
        gamepad = manager.GetGamepad(1);
        dust.Stop();
        sparkA.Stop();
        sparkB.Stop();
        respawnPoint = GetComponent<Transform>().position;
    }

    void FixedUpdate()
    {
        if (!isWin)
        {
            if (!isDead)
            {
                actualSpeed = rb.velocity.magnitude;
                if ((gamepad.GetStick_L().Y > 0.5 || gamepad.GetStick_L().Y < -0.5 || gamepad.GetStick_L().X < -0.5 || gamepad.GetStick_L().X > 0.5))
                {
                    angle = Mathf.Atan2(gamepad.GetStick_L().Y, -gamepad.GetStick_L().X) * 180 / Mathf.PI - 90;
                    direction = new Vector3(0, transform.localEulerAngles.y + angle, 0);
                    anim.SetBool("walking", true);
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
                else
                {
                    anim.SetBool("walking", false);
                    sparkB.Stop();
                    sparkA.Stop();
                }
            }
        }

    }
    IEnumerator pauseAnim()
    {
        while (isPause)
        {
            yield return new WaitForEndOfFrame();
            if (textAnima > 0)
            {
                pauseText.transform.position = new Vector3(textOriginP.x, textOriginP.y + pauseMove.Evaluate(textAnima), textOriginP.z);
                textAnima += 0.01f;
                if (pauseMove.Evaluate(textAnima) <= 0)
                {
                    if (isPause)
                    {

                        textAnima = 0.001f;

                    }
                }
            }
        }
        textAnima = 0;
    }

    public IEnumerator restartCD()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        restart = true;
    }

    void Update()
    {
        if(isWin|| isDead)
        {
            rb.velocity = rb.velocity / 2;
            listeI.enabled = false;
            listeT.enabled = false;
            timerT.enabled = false;
            timerI.enabled = false;
            timer.gameOn = false;
            run.enabled = false;

        }
        if (isDead && gameOverText.enabled == false)
        {
            if(garde)
            {
                gameOverText.text = "you've been caught!";
            }
            else
            {
                gameOverText.text = "The store's now closed, it'll re-open tomorrow";
            }
            gameOverText.enabled = true;
            StartCoroutine(restartCD());
        }
        if (isDead && anim.GetBool("walking"))
        {

            anim.SetBool("walking", false);
            anim.SetTrigger("reboot");
        }
        if(isWin && anim.GetBool("walking"))
        {

            anim.SetBool("walking", false);
            anim.SetTrigger("reboot");
        }

        if (restart)
        {
            pressA.enabled = true;
        }

        if (restart && gamepad.GetButtonDown("A"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (gamepad.GetButtonDown("Start"))
        {
            isPause = !isPause;
            if (isPause)
            {
                pauseText.transform.position = textOriginP;
                textAnima = 0.001f;
                StartCoroutine(pauseAnim());
                Time.timeScale = 0;
                pauseText.enabled = true;
                screen.enabled = true;
            }

            else
            {
                Time.timeScale = 1;
                pauseText.enabled = false;
                screen.enabled = false;
            }
        }


          //boost puis vitesse augmentée
            if (gamepad.GetButtonDown("A") && !bigBoostActive)
            {
                anim.SetTrigger("boost");
                sparkA.Play();
                sparkB.Play();

                StartCoroutine(Turbo());

            }
            if (gamepad.GetButton("A"))
            {
                dust.Play();

                boost = true;
                anim.SetBool("run", true);
            }
            if (!gamepad.GetButton("A"))
            {
                boost = false;
                dust.Stop();
                sparkA.Stop();
                sparkB.Stop();

                anim.SetBool("run", false);
            
        }
    }
            /*
            if (gamepad.GetButtonDown("X") && !jumpRecovery)
            {
                StartCoroutine(jumpTime());
                Jump();
            }
            */
        
	
    IEnumerator Turbo()
    {

        bigBoost = true;
        bigBoostActive = true;
        yield return new WaitForSecondsRealtime(bigBoostDuration);
        
        bigBoost = false;
        yield return new WaitForSecondsRealtime(boostRecovery);
        bigBoostActive = false;
        sparkA.Stop();
        sparkB.Stop();

    }
    IEnumerator jumpTime()
    {
        jumpRecovery = true;
        yield return new WaitForSecondsRealtime(1f);
        jumpRecovery = false;
    }
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
    /*
    void OnCollision(Collision col)
    {
        if (col.collider.gameObject.tag == "garde")
        {
            //on récupère le script
            AI gardeAI = col.collider.gameObject.GetComponent<AI>();

            if (gardeAI.IsAttackMode() == true)
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

}




