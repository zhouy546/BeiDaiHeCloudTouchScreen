using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUnlocker : MonoBehaviour {
    public delegate void UnlockScreen();
    public static event UnlockScreen BeginUnlockScreenEvent;
    public static event UnlockScreen EndUnlockScreenEvent;

    public Image ShineMaterialimg;
    public Color ShineColor;
    public List<Image> LoopImageList;
    public Text UnlockScreenText;

    private string[] UnlockText = { "触摸解锁", "屏幕已解锁" };
    private float ShineIntensity;
    LTDescr a;
    private void OnEnable()
    {
        BeginUnlockScreenEvent += mUnlockScreen;
        BeginUnlockScreenEvent += mSwapeText;
        EndUnlockScreenEvent += mShowMainDisplay;
    }


    private void OnDisable()
    {
        BeginUnlockScreenEvent -= mUnlockScreen;
        BeginUnlockScreenEvent -= mSwapeText;
        EndUnlockScreenEvent -= mShowMainDisplay;

    }

    //initialize event
    public void mBeginUnlockScreenEvent()
    {
        if (BeginUnlockScreenEvent != null)
        {
            BeginUnlockScreenEvent();
        }
    }


    public void mEndUnlockScreenEvent()
    {
        Debug.Log("show mEndUnlockScreenEvent 被调用");

        if (EndUnlockScreenEvent != null)
        {
            EndUnlockScreenEvent();
        }
    }



    // Use this for initialization
    void Start () {
        hoverUnlock();

    }


    void mShowMainDisplay() {
        Debug.Log("show mainDisplay");
    }



    void mUnlockScreen() {
        StartCoroutine(changeRingAlpha(0, 1, .2f, LoopImageList));
        ShineUnlock();
    }

    void mSwapeText() {
        MoveText(UnlockScreenText,60f);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(2)) {
            mBeginUnlockScreenEvent();
        }
	}


    void MoveText(Text text,float moveOffset) {
        Vector3 startpos = text.transform.localPosition;
        Vector3 target = new Vector3(text.transform.localPosition.x, text.transform.localPosition.y - moveOffset, text.transform.localPosition.z);
        //Debug.Log(target);

        LeanTween.moveLocal(text.gameObject, target, .5f).setOnUpdate(delegate (float value)
        {//设置TEXT ALPHA从1-0；
            float alpha = -value;
            ChangeTextAlpha(alpha);
        }).setEase(LeanTweenType.easeInSine).setOnComplete(delegate ()
        {
            //改变TEXT文字信息未已经解锁
            UnlockScreenText.text = UnlockText[1];

            LeanTween.moveLocal(text.gameObject, startpos, .5f).setDelay(.5f).setEase(LeanTweenType.easeInSine).setOnUpdate(delegate(float value) {
                //设置TEXT ALPHA从1-0；
                float alpha = 1 + value;
                ChangeTextAlpha(alpha);
            }).setOnComplete(delegate() {
                //触发完成解锁事件
                mEndUnlockScreenEvent();

                //在Unlockerscreen消失后重新设置解锁文字
            });
        });
    }

    void ChangeTextAlpha(float alpha) {
        UnlockScreenText.color = new Color(UnlockScreenText.color.r, UnlockScreenText.color.g, UnlockScreenText.color.b, alpha);
    }


    void hoverUnlock() {


        a = LeanTween.value(1.5f, 2.5f, 1f).setOnUpdate(delegate (float value)
        {
            ShineIntensity = value;
            Color color = new Color(ShineColor.r*value, ShineColor.g *value, ShineColor.b* value, ShineColor.a* value);
            ShineMaterialimg.material.SetColor("_OutlineColor", color);
        }).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();

    }

    void ShineUnlock() {
        //停止当前的呼吸shine模式；
        LeanTween.cancel(a.id);
        LeanTween.value(ShineIntensity, 5f, .5f).setOnUpdate(delegate (float value)
        {
            Color color = new Color(ShineColor.r * value, ShineColor.g * value, ShineColor.b * value, ShineColor.a * value);
            ShineMaterialimg.material.SetColor("_OutlineColor", color);
        });
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
