using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using UnityEngine;

public class letest : MonoBehaviour {
    public x360_Gamepad gamepad;

    public int papa;

    private GamepadManager manager;
	// Use this for initialization
	void Start () {
        manager = GamepadManager.Instance;
        //  gamepad = manager.GetGamepad();
        gamepad=manager.GetGamepad(papa);


    }

    // Update is called once per frame
    void Update()
    {
        if (gamepad.GetButtonDown("A"))
        {
            StopAllCoroutines();
            Debug.Log("oui" + papa);
            //Debug.Log(papa);
            StartCoroutine(inactif());
        }
    }
    IEnumerator inactif()
    {
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("1");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("2");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("3");
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("trop lent");
    }
        //Debug.Log(papa);
        /*
        if (gamePad2.GetButtonDown("A"))
        {
            Debug.Log("aussi");
        }
        */
    }

