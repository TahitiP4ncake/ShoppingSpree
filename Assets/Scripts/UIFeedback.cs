using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIFeedback : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private float textAnima;
    float textAnimaBis;

    private Vector3 originSize;

    public Text uiText;
    public AnimationCurve selected;
    public AnimationCurve unselected;
	// Use this for initialization
	void Start () {
        originSize = uiText.transform.localScale;
        //uiText.transform.localScale = Vector3.one;
    }
	
	// Update is called once per frame
	void Update () {
        if (textAnima > 0)
        {
            //Debug.Log("coucou");
            uiText.transform.localScale = new Vector3(selected.Evaluate(textAnima), selected.Evaluate(textAnima), 1);
            textAnima += Time.deltaTime;
            if (selected.Evaluate(textAnima) <= 0)
            {
                textAnima = 0;

            }
        }
        if (textAnimaBis > 0)
        {
            uiText.transform.localScale = new Vector3(unselected.Evaluate(textAnimaBis), unselected.Evaluate(textAnimaBis), 1);
            textAnimaBis += Time.deltaTime;
            if (selected.Evaluate(textAnimaBis) <= 0)
            {
                textAnimaBis = 0;

            }
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        //originSize = uiText.transform.localScale;
        textAnima = 0.001f;
        //Debug.Log(uiText.transform.localScale);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        textAnima = 0;
        //textAnimaBis = 0.001f;
        //Debug.Log("deselect");
        //Debug.Log(uiText.transform.localScale);
        uiText.transform.localScale = originSize;
        //Debug.Log(uiText.transform.localScale);
    }

    public void GameOn()
    {
        SceneManager.LoadScene("Controller", LoadSceneMode.Single);
    }

    public void GameOff()
    {
        Application.Quit();
    }
}
