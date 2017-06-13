using UnityEngine;

public class dragObject : MonoBehaviour {

    public Transform papa;
    public float index;

    private Quaternion origin; 
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        origin = transform.rotation;
    }
	void FixedUpdate () {
        if (papa != null)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(papa.position.x, transform.position.y, papa.position.z), 0.5f);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(papa.position.x, transform.position.y, papa.position.z), ref velocity, 0.01f*index/10);
            //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(papa.position.x, papa.position.y+GetComponentInChildren<Renderer>().bounds.extents.y, papa.position.z), ref velocity, 0.01f * index / 5);
            transform.rotation = papa.rotation*origin;
            //.GetComponentInChildren<Renderer>().bounds.extents.y)
        }
	}
}
