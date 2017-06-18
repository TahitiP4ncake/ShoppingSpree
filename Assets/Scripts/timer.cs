using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Text timerText;
    public AnimationCurve Bip;
    public AnimationCurve lowBip;

    public bool gameOn;

    public int seconde;
    private int minute;

    private float textAnima;
    private float textLow;

    public Shadow ombre;
    private bool ending;
    private bool gameOver;
    public CaddieController papa;

	void Start () {
        while(seconde>60)
        {
            minute += 1;
            seconde -= 60;
        }
        timerText.text = minute + " : " + seconde;
        Launch();
	}
	
    public void Launch()
    {
        StartCoroutine(timeOn());
    }

	IEnumerator timeOn()
    {
        while(gameOn)
        {
            yield return new WaitForSecondsRealtime(1);
            if (!papa.isPause)
            {
                
                seconde -= 1;
                if (seconde == 0 && minute != 0)
                {
                    seconde += 60;
                    minute -= 1;
                }
                if (minute == 0 && seconde < 30)
                {
                    textAnima = 0.001f;
                    if (!ending)
                    {
                        ending = true;
                        timerText.color = new Color32(206, 45, 88, 255);
                        ombre.effectColor = new Color32(158, 28, 73, 255);
                    }
                }

                timerText.text = minute + " : " + seconde;
                if (minute == 0 && seconde == 0)
                {
                    timerText.text = "CLOSED";
                    gameOn = false;
                    if(!gameOver)
                    {
                        gameOver = true;
                        papa.isDead = true;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (textAnima > 0)
        {
            timerText.transform.localScale = new Vector3(Bip.Evaluate(textAnima), Bip.Evaluate(textAnima), 1);
            textAnima += Time.deltaTime;
            if (Bip.Evaluate(textAnima) <= 0)
            {
                textAnima = 0;
                
            }
        }
        
    }
	
}
