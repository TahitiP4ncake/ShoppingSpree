using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientMaster : MonoBehaviour {

    public Animator client;
    private bool alive = true;

	void Start () {
        StartCoroutine(Idle());
	}
	
	void Update () {
		
	}

    IEnumerator Idle()
    {
        while(alive)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2, 6));
            int _choice = Random.Range(1, 4);
            if(_choice==1)
            {
                client.SetTrigger("1");
            }
            if (_choice == 2)
            {
                client.SetTrigger("2");
            }
            if (_choice == 3)
            {
                client.SetTrigger("3");
            }
        }

    }
}
