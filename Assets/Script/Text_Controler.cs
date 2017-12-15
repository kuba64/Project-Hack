using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;


public enum Status {norm,yes, no}

[System.Serializable]
public class Perfix
{
    public string text;
    public Color color;
}


public class Text_Controler : MonoBehaviour {
    public Text text;
    public InputField inputfield;
    public float interlinia = 15.96f;
    public float limit;
    public List<Perfix> perfix;


    public float startY;
    string lastLine, comandNeedConfirm;
    Status confirm;

    ControlBox connectedBox;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        ComandActive();
       // startY = inputfield.GetComponent<RectTransform>().localPosition.y;
    }
  
    // Update is called once per frame
    void Update () {
        if (transform.parent.GetComponent<PC_Controler>().run && Input.GetKeyDown(KeyCode.UpArrow))
            inputfield.text = lastLine;
	}

    public void WriteText(string wText)
    {
       // Text newText=null;

        foreach(Perfix perf in perfix)
        {
            if (wText.Contains(perf.text))
            {
                wText= wText.Insert(wText.IndexOf(perf.text),"<color=#" + ColorUtility.ToHtmlStringRGBA(perf.color) + ">");
                wText = wText.Insert(wText.IndexOf(perf.text)+perf.text.Length, "</color>");
                print(wText);
              //  inputfield.transform.position -= new Vector3(0, interlinia);
             //   if (inputfield.GetComponent<RectTransform>().localPosition.y < limit)
                //    ClearConsole();

               // if(perfix[perfix.Count-1]==perf)

               
               
            }
          
        }
        

        text.text += wText + "\n";
        inputfield.transform.position -= new Vector3(0, interlinia);
        if (inputfield.GetComponent<RectTransform>().localPosition.y < limit)
            ClearConsole();  

    }

    public void ConfirmComand()
    {

            WriteText(inputfield.text);
            FindComand(inputfield.text);
            lastLine = inputfield.text;
            inputfield.text = "";
            ComandActive();
    }

     public void ClearConsole()
    {
        text.text = null;
        inputfield.GetComponent<RectTransform>().localPosition = new Vector3(inputfield.GetComponent<RectTransform>().localPosition.x, startY);
        ComandActive();
        
    }
    

    void FindComand(string comand)
    {
        switch(comand)
        {
            case "/help":
                WriteText("/clear");
                WriteText("/exit");
                break;

            case "/exit":
                NeedComfirm("Close?", "Closing...", "Canceled", comand, new Action(transform.parent.GetComponent<PC_Controler>().ClosePC));
                break;

            case "y":
                if (comandNeedConfirm != null)
                {
                    confirm = Status.yes;
                    FindComand(comandNeedConfirm);
                    confirm = Status.norm;
                }
                else
                    FindComand("");

                break;


            case "n":
                if (comandNeedConfirm != null)
                {
                    confirm = Status.no;
                    FindComand(comandNeedConfirm);
                    confirm = Status.norm;
                }
                else
                    FindComand("");
                break;


            case "/clear":
                ClearConsole();
                break;

            case "finddevice -hacked":
                FindDevice(true);
                break;

            case "finddevice -all":
                FindDevice(false);
                break;

            case "":
                 break;

            case "showdevices -connected":
                ShowDevices(connectedBox);
                break;

            case "disconnect":
                WriteText("Disconnected");
                connectedBox = null;
                break;


            default:
              //  print("ja");
             //   WriteText("Wrong comand!!!");
                break;            
        }

        if(comand.StartsWith("connect "))
        {
            ControlBox[] cb = FindObjectsOfType<ControlBox>();
            foreach (ControlBox c in cb)
                if (comand.Contains(c.IP) && comand.EndsWith(c.password))
                {
                    WriteText("Connected");
                    connectedBox = c;
                }
                else if (comand.Contains(c.IP) && !comand.EndsWith(c.password))
                    WriteText("Bad password");
                else
                    WriteText("Problem with connect");
        }

        foreach (GameObject a in connectedBox.connectedDevice)
            if(comand==(a.name+".")|| comand == (a.name))
            {
                if(a.GetComponent<LightControl>())
                {
                    WriteText("on");
                    WriteText("off");
                }
            }
            else if(comand.StartsWith(a.name + "."))
            {
                if (a.GetComponent<LightControl>())
                {
                    if (comand == a.name + ".on")
                    {
                        a.GetComponent<LightControl>().isOn = true;
                        WriteText("Success");
                    }
                    else if (comand == a.name + ".off")
                    {
                        a.GetComponent<LightControl>().isOn = false;
                        WriteText("Success");
                    }
                }
            }
    }

    void ShowDevices(ControlBox control)
    {
        foreach (GameObject a in control.connectedDevice)
            WriteText(a.name);
    }

    void FindDevice(bool hacked)
    {
        ControlBox[] cb = FindObjectsOfType<ControlBox>();
        foreach (ControlBox c in cb)
            if (c.hacked == true&&hacked)
                WriteText(c.IP + " " + c.password);
            else if(c.hacked == true && !hacked)
                WriteText(c.IP + " " + c.password);
           else if (c.hacked == false && !hacked)
                WriteText(c.IP);
    } 

    void NeedComfirm(string ask, string yes,string no, string com, Action action)
    {
        if (confirm == Status.norm)
        {
            WriteText(ask+" (y-yes, n-no)");
            comandNeedConfirm = com;
            
        }
        if (confirm == Status.yes)
        {
            WriteText(yes);
            comandNeedConfirm = null;
            action();
        }
        if (confirm == Status.no)
        {
            WriteText(no);
            comandNeedConfirm = null;
        }
    }

    public void ComandActive()
    {
        EventSystem.current.SetSelectedGameObject(inputfield.gameObject, null);
        inputfield.OnPointerClick(new PointerEventData(EventSystem.current));
    }
}
