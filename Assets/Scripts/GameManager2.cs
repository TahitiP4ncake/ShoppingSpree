using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour {

	public Text list;
	public string[] itemlistName;
	public int[] itemlistMax;
	public int[] itemlistCount;
	public int itemNumber = 20;
    public float timeLeft;
    public Text time;
    public GameObject losePanel;


	void Start () {
		int n = 0;
        while (n < itemNumber) {
            int i = Mathf.FloorToInt(Random.value * itemlistName.Length);
            if (itemlistMax[i] < 5)
            {
                itemlistMax[i]++;
                n++;
            }
		}
	}
	

	void Update () {
		list.text = "<b>Liste de course :</b>";
		for(int i=0; i<itemlistName.Length; i++){
			list.text += "\n" + itemlistName [i] + "  " + itemlistCount[i]+"/"+itemlistMax[i];
		}
        if (timeLeft > 0)
            timeLeft -= Time.deltaTime;
        /*
        else if (losePanel.activeSelf == false)
        {
            
            losePanel.SetActive(true);
            foreach (PlayerControl p in FindObjectsOfType<PlayerControl>())
            {
                p.controlOverride = true;
            }
            
        }
        */
        time.text = StringTime(timeLeft);
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

    public string StringTime(float _time)
    {
        string sec = (Mathf.Floor(_time) % 60).ToString();
        if (sec.Length < 2)
        {
            sec = "0" + sec;
        }
        return Mathf.Floor(_time / 60).ToString() + ":" + sec;
    }
}
