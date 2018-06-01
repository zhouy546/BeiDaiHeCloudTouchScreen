using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageMask : UIImageBase
{
    public float currentAmount;
    public float defaultAmount;
	// Use this for initialization
   void Start () {
        base.Start();

        initialization();
    }

    // Update is called once per frame
    void Update () {
        base.Update();
    }

    public override void initialization() {

        image = this.GetComponent<Image>();
        CurrentColor=Defaultcolor = image.color;
        ChangeColor(0, Defaultcolor);
        HideImage();
        currentAmount = defaultAmount = image.fillAmount;
        setFill(1, 0);
    }

    public void ShowImageMask() {
        setFill(1, 1);
    }

    public void HideImageMask() {
        setFill(0, 1);
    }

    public void setFill(float amout,float time) {

        LeanTween.value(currentAmount, amout, time).setEase(LeanTweenType.easeOutSine).setOnUpdate(delegate (float value) {
            image.fillAmount = value;
        }).setOnComplete(delegate() {
            currentAmount = image.fillAmount;
        });
    }

    public override void ResetItem()
    {
        initialization();

    }



}
