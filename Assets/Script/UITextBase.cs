using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class UITextBase : MonoBehaviour {

    protected Text text;
    public bool onDisplay;
    public Color Defaultcolor;
    public Color CurrentColor;

    public string DefaultText="默认文字";
    public string CurrentText;

    public delegate void setDisplayValue();

    public virtual void initialization()
    {
        text = this.GetComponent<Text>();
        CurrentColor = Defaultcolor = text.color;
        HideText( 0);
    }
    // Use this for initialization
    public virtual void Start () {
        initialization();
    }
	
	// Update is called once per frame
  	public virtual void Update () {
		
	}

    public virtual void ShowText(float time) {
        ChangeTextAlpha(1, time, ()=> onDisplay =true);
    }

    public virtual void HideText(float time) {
        ChangeTextAlpha(0, time, () => onDisplay = false);
    }

   public void ChangeTextAlpha(float alpha, float time, setDisplayValue Displayvalue=null) {
        LeanTween.value(CurrentColor.a, alpha, time).setEase(LeanTweenType.easeInSine).setOnUpdate(delegate (float value) {
            text.color = new Color(CurrentColor.r, CurrentColor.g, CurrentColor.b, value);
        }).setOnComplete(delegate () {
            Displayvalue();
            CurrentColor = new Color(CurrentColor.r, CurrentColor.g, CurrentColor.b, alpha);
        });
    }

    public void SetText(string str) {
        text.text = str;
    }

    public void ChangeTextColor(float time, Color color) {
        LeanTween.color(this.gameObject, color, time).setEase(LeanTweenType.easeInSine).setOnComplete(delegate ()
        {
            CurrentColor = color;
        });
    }


    public void ResetItem()
    {
        ChangeTextColor(0, Defaultcolor);
        initialization();
    }

}
