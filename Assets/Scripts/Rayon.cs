using System.Collections;
using UnityEngine;

public class Rayon : MonoBehaviour {

    public GameObject objet;
    public float Cooldown = 2;

    public bool picked;



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
}
