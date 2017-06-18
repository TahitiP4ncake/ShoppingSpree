using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class controlImage : MonoBehaviour, ISelectHandler,IDeselectHandler// required interface when using the OnSelect method.
{

    public Image controls;
    public Image screen;
    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        controls.enabled = true;
        screen.enabled = true;
        //Debug.Log(this.gameObject.name + " was selected");
    }
    public void OnDeselect(BaseEventData eventData)
    {
        controls.enabled = false;
        screen.enabled = false;

    }


    public void ShowControl()
    {
        if(controls.enabled==false)
        {
            controls.enabled = true;
        }
        else
        {
            controls.enabled = false;
        }
    }
}
