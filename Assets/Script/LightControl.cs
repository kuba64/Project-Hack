using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {
    public bool isOn = true;
    public Sprite on, off;
    public GameObject splash;

    SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isOn)
            On();
        else
            Off();
                
	}
     void On()
    {
        sprite.sprite = on;
        splash.SetActive(true);
    }
     void Off()
    {
        sprite.sprite = off;
        splash.SetActive(false);
    }
}
