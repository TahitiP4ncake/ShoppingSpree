using System.Collections;
using UnityEngine;

public class Rayon : MonoBehaviour {

    public GameObject objet;
    public GameObject Pointer;

    public float Cooldown = 2;

    public bool picked;

    void Start()
    {
        pointerOn();
    }

	public void PickedUp()
    {
        picked = true;
        StartCoroutine(PickedCD());
    }

    IEnumerator PickedCD()
    {
        yield return new WaitForSecondsRealtime(Cooldown);
        picked = false;
        
    }

    public void pointerOn()
    {
        GameObject _pointer = Instantiate(Pointer, transform);
       // _pointer.GetComponent<Rigidbody>().useGravity=false;
       // _pointer.GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
       // _pointer.GetComponent<Rigidbody>().angularDrag = 0.01f;
       // _pointer.transform.localScale += new Vector3(2,2,2);
        _pointer.transform.position += new Vector3(0, 5, 0);
    }
}
