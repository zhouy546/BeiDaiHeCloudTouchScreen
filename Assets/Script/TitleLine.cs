using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleLine : UIImageBase {
    float currentFillamount;
	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    public override void initialization()
    {
        image = this.GetComponent<Image>();
        CurrentColor = Defaultcolor = image.color;
        ChangeColor(0, Defaultcolor);
        ShowImage();
        image.fillAmount = 0;
    }

    public void Show()
    {
        setFillAmount(currentFillamount, 1,.5f);

    }

    public void Hide()
    {
        setFillAmount(currentFillamount, 0, .5f);
    }


    public void setFillAmount(float from, float to , float time) {
        LeanTween.value(from, to, time).setEase(LeanTweenType.easeInQuad).setOnUpdate(delegate (float value) {
            image.fillAmount = value;
            currentFillamount = value;
        });
    }
}
