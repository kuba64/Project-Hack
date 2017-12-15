using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Controler : MonoBehaviour {
    public Text_Controler CMD;
    [HideInInspector]
    public bool run;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TurnPC()
    {
        if (run)
            ClosePC();
        else
        {
            run = true;
            gameObject.SetActive(true);
            CMD.ComandActive();
            StartCoroutine(Wait(2));
            CMD.WriteText("Starting PC");
            StartCoroutine(Wait(1));
            CMD.WriteText("Loadind Data.............");
            StartCoroutine(Wait(1));
            CMD.WriteText("WELCOME!!!");
            StartCoroutine(Wait(1));
            CMD.WriteText("(write '/help' if you need help)");
            StartCoroutine(Wait(1));
        }

    }


    public void ClosePC()
    {
        CMD.ClearConsole();
        gameObject.SetActive(false);
        run = false;

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
