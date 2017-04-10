using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioFeeback : MonoBehaviour {
    private GamepadManager manager;
    public AudioSource A_Horn;
    public SoundManager A_manager;
    public float startingPitch;

    private float _localPitch;
	// Use this for initialization
    void Awake()
    {

        A_Horn = A_manager.GetComponent<AudioSource>();
        manager = GamepadManager.Instance;
        _localPitch = startingPitch + Random.Range(-0.1f, 0.1f);
        A_manager.Source.Add(gameObject.GetComponent<AudioSource>());

    }
	void Start () {
        
        

    }
	
	// Update is called once per frame
	void Update () {
        if (manager.GetButtonDownAny("Y"))
        {
            Horn();
        }
    }
    
    void Horn()
    {
        Debug.Log("coucou");
        A_Horn.pitch = _localPitch;
        Harmony.Play(A_Horn);
    }
}
