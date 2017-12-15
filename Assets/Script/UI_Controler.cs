using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controler : MonoBehaviour {
    public GameObject progressBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ProgressBar(GameObject hacked, float progress)
    {
      //  if (progressBar.activeInHierarchy)
      //  {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, hacked.transform.position);
            ((RectTransform)progressBar.transform).anchoredPosition = screenPoint - ((RectTransform)transform).sizeDelta / 2f;
        ((RectTransform)progressBar.transform).anchoredPosition += new Vector2(0,35);
            progressBar.SetActive(true);
      //  }
       // else
      //  {
            progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = progress;
       // }
      //
    }
    public void ProgressBarOff()
    {
        progressBar.SetActive(false);
    }
}
