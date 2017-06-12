using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PickUp : MonoBehaviour {

	public string itemName;
	public Collider[] colliders;
	public bool pikedUp;
	public float height = 1;

	public void SetColliders(bool _enable){
		foreach(Collider c in colliders){
			c.enabled = _enable;
		}
	}
}
