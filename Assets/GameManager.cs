using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text list;
	public string[] itemlistName;
	public int[] itemlistMax;
	public int[] itemlistCount;
	public int itemNumber = 10;

	// Use this for initialization
	void Start () {
		int n = 0;
		while (n<itemNumber){
			itemlistMax [Mathf.FloorToInt (Random.value * itemlistName.Length)]++;
			n++;
		}
	}

	// Update is called once per frame
	void Update () {
		list.text = "<b>Liste de course :</b>";
		for(int i=0; i<itemlistName.Length; i++){
			list.text += "\n" + itemlistName [i] + "  " + itemlistCount[i]+"/"+itemlistMax[i];
		}
	}

	public int FindItemIndex(PickUp _item){
		for(int i=0; i<itemlistName.Length; i++){
			if (itemlistName [i] == _item.itemName)
				return i;
		}
		return -1;
	}

	public float CalculateAccuracy(){

		float total = 0;
		int n = itemlistName.Length;
		float[] itemlistAcc = new float[n];
		for(int i=0; i<n; i++){
			int needed = itemlistMax[i];
			int diff = Mathf.Abs (itemlistCount[i] - needed);
			itemlistAcc[i] = Mathf.Abs (1f - (1f / needed * diff));
		}
		for(int i=0; i<n; i++){
			total += itemlistAcc [i];
		}
		total /= n;
		return Mathf.Max(0,total);
	}
}
