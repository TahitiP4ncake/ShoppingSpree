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
    private Vector3 originText;

    //screenshake
    public float duration = .1f;
    public float magnitude = 1;
    private bool isShaking;



    void Start () {
        papa = transform.parent;
        origin = transform.localPosition;
        originR = transform.localRotation;
        originText = perdu.transform.position;
        //perdu.transform.position = new Vector3(perdu.transform.position.x, perdu.transform.position.y - 1, perdu.transform.position.z);
	}
	

	void Update ()
    {

		if(gameOverOn)
        {
            /*
            if(!isShaking)
            {
                isShaking = true;
                StartCoroutine(Shake());
            }
            */
            CloseUp();
            //transform.position = transform.parent.position;
            transform.position = Vector3.SmoothDamp(transform.position, transform.parent.position,ref velocity,.8f);
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.parent.rotation, 2f * Time.deltaTime);
            //transform.LookAt(target);
            //perdu.transform.position = Vector3.SmoothDamp(perdu.transform.position, originText, ref velocity,0.5f);
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
            //isShaking = false;
            
            //GetComponent<Camera>().transform.SetParent(papa);
            //transform.localPosition = origin;
            //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, origin, ref velocity, 0.01f);
            //transform.localRotation = originR;
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

    public IEnumerator Shake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.position;
        if (!isShaking)
        {
            isShaking = true;
            while (elapsed < duration)
            {

                elapsed += Time.deltaTime;

                float percentComplete = elapsed / duration;
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

                // map value to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;
                x *= magnitude * damper;
                y *= magnitude * damper;

                transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);

                yield return null;
            }
            isShaking = false;
        }
        transform.position = originalCamPos;
    }
}
