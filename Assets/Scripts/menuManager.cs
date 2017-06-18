using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuManager : MonoBehaviour {

    private GamepadManager manager;
    public x360_Gamepad gamepad;

    public int choice = 3;
    bool wait;

    public Button play;
    public Button controls;
    public Button quit;

    public Sprite playN;
    public Sprite playH;
    public Sprite playC;

    public Sprite controlN;
    public Sprite controlH;
    public Sprite controlC;

    public Sprite quitN;
    public Sprite quitH;
    public Sprite quitC;

    void Start () {
        manager = GamepadManager.Instance;
        gamepad = manager.GetGamepad(1);

        
    }
	
	void Update () {

        

        CheckInput();

        if (choice == 3 && !gamepad.GetButton("A") && !wait)
        {
            StartCoroutine(menuCD());
            play.image.sprite = playH;
            controls.image.sprite = controlN;
            quit.image.sprite = quitN;
        }

        if (choice == 2 && !gamepad.GetButton("A") && !wait)
        {
            StartCoroutine(menuCD());
            play.image.sprite = playN;
            controls.image.sprite = controlH;
            quit.image.sprite = quitN;
        }

        if (choice == 1 && !gamepad.GetButton("A") && !wait)
        {
            StartCoroutine(menuCD());
            play.image.sprite = playN;
            controls.image.sprite = controlN;
            quit.image.sprite = quitH;
        }
    }

    void CheckInput()
    {
        if(gamepad.GetStick_L().Y>0)
        {
            Down();
        }
        if (gamepad.GetStick_L().Y < -0)
        {
            Up();
        }
    }

    void Up()
    {
        choice += 1;
       if(choice>=4)
        {
            choice = 1;
        }
    }

    void Down()
    {
        choice -= 1;
        if (choice <= 0)
        {
            choice = 3;
        }
    }

    IEnumerator menuCD()
    {
        wait = true;
        yield return new WaitForSecondsRealtime(1f);
        wait = false;
    }
}
