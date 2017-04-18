using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class gameOver : MonoBehaviour {

    public Transform gOTransform;

    public bool gameOverOn;
    public Transform target;

    private Transform papa;
    private Vector3 origin;
  
    private Quaternion originR;
    private Vector3 velocity = Vector3.zero;

    public Image rouge;
    private Color aColor;
    //Red Screen

    bool increment = true;
    float len = 1.5f;
    float opac = 0.4f;


    // Text vigile
    public Text perdu;

    void Start () {
        papa = transform.parent;
        origin = transform.localPosition;
        originR = transform.localRotation;
	}
	

	void Update ()
    {
		if(gameOverOn)
        {
            CloseUp();
            //transform.position = transform.parent.position;
            transform.position = Vector3.SmoothDamp(transform.position, transform.parent.position,ref velocity,0.1f);
            transform.LookAt(target);

            //red fade
            aColor = rouge.color;
            if (increment == true)
            {
                aColor.a += opac * Time.deltaTime / len;

            }
            else
            {
                aColor.a -= opac * Time.deltaTime / len;
            }

            if (aColor.a > opac)
            {
                increment = false;
            }
            else if (aColor.a < .1f)
            {
                increment = true;
            }

            rouge.color = aColor;

            perdu.enabled = true;
        }
        else
        {
            GetComponent<Camera>().transform.SetParent(papa);
            //transform.localPosition = origin;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, origin, ref velocity, 0.3f);
            transform.localRotation = originR;
            aColor = rouge.color;
            aColor.a = 0;
            rouge.color = aColor;
            perdu.enabled = false;
        }

        

    }

    void CloseUp()
    {
        
        GetComponent<Camera>().transform.SetParent(gOTransform);
    }
}
