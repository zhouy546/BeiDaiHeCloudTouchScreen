using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheUIManger : MonoBehaviour {
    public Material UnlockMaterial;
    public Color ShineColor;
    public List<Image> LoopImageList;
	// Use this for initialization
	void Start () {
        hoverUnlock();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(2)) {
            StartCoroutine(changeRingAlpha(0,1,.2f, LoopImageList));
        }
	}


    void hoverUnlock() {

        LeanTween.value(1.5f, 2.5f, 1f).setOnUpdate(delegate (float value)
        {
            Color color = new Color(ShineColor.r*value, ShineColor.g *value, ShineColor.b* value, ShineColor.a* value);
            UnlockMaterial.SetColor("_OutlineColor", color);
        }).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();

    }


    IEnumerator changeRingAlpha(float from, float to,float time, List<Image> images ) {


        foreach (var image in images)
        {
            Color currentColor = image.color;
            LeanTween.value(from, to, time).setOnUpdate(delegate (float value)
            {
                image.color = new Color(currentColor.r, currentColor.g, currentColor.b, value);
            }).setOnComplete(delegate () {

                LeanTween.value(to, from, time).setOnUpdate(delegate (float value)
                {
                    image.color = new Color(currentColor.r, currentColor.g, currentColor.b, value );
                });

            });
            yield return new WaitForSeconds(time / 2);
        }

  

    } 
}
