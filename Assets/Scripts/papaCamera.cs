using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class papaCamera : MonoBehaviour {

    public GameObject papa;
    private Vector3 offset;
    private Quaternion orientation;
    private Vector3 velocity = Vector3.zero;
    public float SmoothTime = 0.1f;
    public float duration=.1f;
    public float magnitude=1;
	void Start () {
        offset = transform.position - papa.transform.position;
        orientation = transform.rotation * papa.transform.rotation;
       
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(bumpWall);
        //transform.position = papa.transform.position + offset;
        //transform.position = Vector3.SmoothDamp(transform.position,papa.transform.position + offset,ref velocity, SmoothTime);
        //transform.eulerAngles = papa.transform.eulerAngles + orientation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, papa.transform.rotation, 0.1f);
       
    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, papa.transform.position + offset, ref velocity, SmoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, papa.transform.rotation, 0.1f+Time.deltaTime);
    }
    IEnumerator Shake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.position;

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

            transform.position = new Vector3(originalCamPos.x+x, originalCamPos.y+y, originalCamPos.z);

            yield return null;
        }

        transform.position = originalCamPos;
    }
}
