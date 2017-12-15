using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBox : MonoBehaviour {
    public string IP, password;
    public GameObject[] connectedDevice;
    public GameObject[] cables;
    public bool hacked;
    public float hackTime;

    bool enter;
    UI_Controler ui;
    float time;
    Animator anim;
	// Use this for initialization
	void Start () {
        ui = FindObjectOfType<UI_Controler>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q)&&enter&&!hacked)
            Hack();
       else
            HackOff();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enter = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enter = false;
    }

    public void Hack()
    {
        time += Time.deltaTime;
        ui.ProgressBar(gameObject, time/hackTime);
        if (time >= hackTime)
        {
            foreach(GameObject c in cables)
                for(int i =0; i < c.transform.childCount; i++)
                    c.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = c.transform.GetChild(i).GetComponent<Cable>().hacked;

            hacked = true;
            anim.SetBool("hacked", hacked);
            HackOff();
        }
    }
    void HackOff()
    {
        time = 0;
        ui.ProgressBarOff();
    }
}
