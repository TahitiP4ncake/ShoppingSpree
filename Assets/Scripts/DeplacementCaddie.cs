using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementCaddie : MonoBehaviour {
    public Rigidbody Caddie;
    public GameObject Capsule;

    public float TurnSpeed = 2f;
    public float Speed = 1f;

    public int Amount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float _turn = Input.GetAxis("LJoystickX");
        float _moveForward = Input.GetAxis("LJoystickY")*-20;
        
        float _lookHorizontal = Input.GetAxis("RJoystickX") * 60;
        if (Input.GetAxis("LJoystickX") >0.5 || Input.GetAxis("LJoystickX")<-0.5)
        {
            _turn = Input.GetAxis("LJoystickX") * 30;
        }
        //Debug.Log(_turn);

        //rotation ?
        float turn = _turn * TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        Caddie.MoveRotation(Caddie.rotation * turnRotation);

        Quaternion rotation = Quaternion.Euler(0, (_lookHorizontal + turn)/2, 0);
          Vector3 direction = rotation * (new Vector3 (0,0,_moveForward*Speed));
       // Caddie.transform.rotation = rotation;
          Caddie.AddRelativeForce(direction);
      //  Vector3 movement = transform.forward * _moveForward * Speed ;
        //Debug.Log(movement);
        //Caddie.MovePosition(Caddie.position + movement);
       // Caddie.AddForce((Caddie.position + movement));
        // rigidbody.AddForce((whateverObject.transform.position - transform.position) * someFactor * Time.smoothDeltaTime);
        // Debug.Log(Capsule);

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "recuperer")
        {
            Destroy(other.gameObject);
            Amount++;
        }
    }
}
/*
Quaternion rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
Vector3 direction = rotation * Vector3.transform.forward;


  Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * _moveForward * Speed * Time.deltaTime;

      Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

    float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

    Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

     m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
*/
