using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class UIImageBase : MonoBehaviour {
    public Color Defaultcolor;
    public Color CurrentColor;

    public Image image;
    public bool onDisplay;

    public delegate void setDisplayValue();

    protected LTDescr alphaLTDescr;

    // Use this for initialization
    public virtual void Start () {
        initialization();
    }

    // Update is called once per frame
    public virtual void Update () {
		
	}

    public virtual void initialization() {

        image = this.GetComponent<Image>();
        CurrentColor=Defaultcolor = image.color;
        ChangeColor(0, Defaultcolor);
        HideImage();
    }

    public void HideImage() {
        changeImageAlpha(0, 0, () => onDisplay = false);
    }

    public void ShowImage()
    {
        changeImageAlpha(1, 0, () => onDisplay = true);
    }

    public void changeImageAlpha(float alpha, float time , setDisplayValue Displayvalue) {
        alphaLTDescr= LeanTween.value(CurrentColor.a, alpha, time).setEase(LeanTweenType.easeInSine).setOnUpdate(delegate (float value) {
            image.color = new Color(CurrentColor.r, CurrentColor.g, CurrentColor.b, value);
        }).setOnComplete(delegate () {       
               CurrentColor = new Color(CurrentColor.r, CurrentColor.g, CurrentColor.b, alpha);
            Displayvalue();
        });
    }

    public void ChangeColor(float time, Color color) {
        LeanTween.color(this.gameObject, color, time).setEase(LeanTweenType.easeInSine).setOnComplete(delegate ()
        {
            CurrentColor = color;
        });
    }

    public void cancelAlphaTween() {
        LeanTween.cancel(alphaLTDescr.id);
    }

    public virtual void ResetItem()
    {
        initialization();
    }

    public virtual void OnAlphaComplete() {

    }

}
