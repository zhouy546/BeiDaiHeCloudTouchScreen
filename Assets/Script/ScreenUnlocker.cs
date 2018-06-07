using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUnlocker : MonoBehaviour {

    public static ScreenUnlocker instance;

    public delegate void UnlockScreen();

    public static event UnlockScreen initilizeUnlockScreen;
    public static event UnlockScreen BeginUnlockScreenEvent;
    public static event UnlockScreen InbetweenUnlockScreenEvent;
    public static event UnlockScreen EndUnlockScreenEvent;


    public float TextHideDelay, TextPosOffset, EndUnlockTrigerDelayTime,LoopRingDelay;

    public Image ShineMaterialimg;
    public Color ShineColor;
    public List<Image> LoopImageList;
    public Text UnlockScreenText;
    public Image ImgScaner;


    public float countDownTime;
    public float currentCountdownTime;
    public bool inLock =true;
    private List<Image> DisplayImages ;

    private string[] UnlockText = { "触摸解锁", "屏幕已解锁" };
    private Vector3 Textstartpos, Texttarget;
    private float ShineIntensity;

    LTDescr a;


    void SubScribe() {
        BeginUnlockScreenEvent += mUnlockScreen;
        BeginUnlockScreenEvent += mHideText;
        InbetweenUnlockScreenEvent += mShowText;
        EndUnlockScreenEvent += endunlock;
        initilizeUnlockScreen+=ResetUnlocker;
    }

    void UnSubScribe() {
        BeginUnlockScreenEvent -= mUnlockScreen;
        BeginUnlockScreenEvent -= mHideText;
        InbetweenUnlockScreenEvent -= mShowText;
        EndUnlockScreenEvent -= endunlock;
        initilizeUnlockScreen -=ResetUnlocker;


    }

   public  void endunlock() {
        MainUI.instance.TurnOnAllNodeInteraction();
        MainControler.instance.SetState(MainControler.instance.PerviousState);
        MainControler.instance.turnonMovement();
        mHideLockerScreen();
        StartCountDown();
        mHideText();
    }

    private void OnEnable()
    {
        SubScribe();
    }


    private void OnDisable()
    {
        UnSubScribe();
    }


    public void minitilizeUnlockScreen()
    {
        Debug.Log("唤醒屏保");

        if (initilizeUnlockScreen != null)
        {
            initilizeUnlockScreen();
        }
    }

    //initialize event
    public void mBeginUnlockScreenEvent()
    {
        Debug.Log("开始解锁屏幕被调用");

        if (BeginUnlockScreenEvent != null)
        {
            BeginUnlockScreenEvent();
        }
    }


    public void mEndUnlockScreenEvent()
    {
        Debug.Log("结束解锁被调用");

        if (EndUnlockScreenEvent != null)
        {
            EndUnlockScreenEvent();
        }
    }


    public void mInbetweenUnlockScreenEvent()
    {
        Debug.Log("解锁中被调用");

        if (EndUnlockScreenEvent != null)
        {
            InbetweenUnlockScreenEvent();
        }
    }


    // Use this for initialization
    void Start () {
        if (instance == null) {
            instance = this;
        }

        initialization();

    }

    public void initialization() {
        Textstartpos = UnlockScreenText.transform.localPosition;

        hoverUnlock();

        DisplayImages = UtilityFun.instance.GetDisplayImage(this.gameObject, LoopImageList);

        currentCountdownTime = countDownTime;

        StartCoroutine(countDown());

    }

    public void ResetUnlocker() {
        hoverUnlock();
        List<Image> temp = LoopImageList;
        temp.Add(ImgScaner);

        List<Image> displaytemp = UtilityFun.instance.GetDisplayImage(this.gameObject, temp);

        UtilityFun.instance.ChangeListOfImageAlpha(displaytemp, 1, .5f);

        UtilityFun.instance.ChangeListOfImageAlpha(temp, 0, 0);

        scanerAlpha(ImgScaner, 0);

        MovingText(UnlockScreenText, TextPosOffset, false);

        UnlockScreenText.text = UnlockText[0];

        this.GetComponent<Image>().raycastTarget = true;//set it interactable

        MainControler.instance.SetState(AppState.Looker);

    }


    public void StartCountDown() {

        currentCountdownTime = countDownTime;
        inLock = false;
        StartCoroutine(countDown());

    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetMouseButtonDown(2))
       // {
       //mBeginUnlockScreenEvent();
       // }
        //Debug.Log(ShineIntensity);
    }

    //    隐藏UI界面  订阅EndUnlockScreenEvent结束锁屏事件
    void mHideLockerScreen() {
        UtilityFun.instance.ChangeListOfImageAlpha(DisplayImages, 0,.5f);
        UtilityFun.instance.ChangeShineIntensity(ref ShineIntensity, 0f,.5f, ShineColor, ShineMaterialimg);
    }

    //向上移动SCANER
    void mMoveScanerUp(Image image,float time) {
        float target = (image.rectTransform.rect.width - image.rectTransform.rect.height)/2;
        Vector3 pos = new Vector3(image.transform.localPosition.x, target, image.transform.localPosition.x);

        LeanTween.moveLocal(image.gameObject, pos, time).setOnUpdate( (float value)=>{
            scanerAlpha(image, value);
        }).setOnComplete(()=> mMoveScanerDown(time));
    }

    //向下移动SCANER
    void mMoveScanerDown(float time)
    {
        //触发inbetween事件

        mInbetweenUnlockScreenEvent();

        float target = -ImgScaner.rectTransform.rect.width/ 2;
        Vector3 pos = new Vector3(ImgScaner.transform.localPosition.x, target, ImgScaner.transform.localPosition.x);
        //SCANER开始向下运动
        LeanTween.moveLocal(ImgScaner.gameObject, pos,time ).setOnUpdate((float value) => {

            float newvalue = 1 - value;
            scanerAlpha(ImgScaner, newvalue);
        }).setOnComplete(delegate() {
            //出发结束锁屏事件
            mEndUnlockScreenEvent();
            });
    }

    //设置SCANER透明度
    void scanerAlpha(Image img, float value)
    {
        float instensity = 3;
        Color color = new Color(img.color.r, img.color.g, img.color.b, value);
        img.color = color;

        Color HDRcolor = new Color(img.color.r * value * instensity, img.color.g * value * instensity, img.color.b * value * instensity, img.color.a * value * instensity);
        img.material.SetColor("_OutlineColor", HDRcolor);
    }

    //开始解锁屏幕 订阅BeginUnlockScreenEvent
    void mUnlockScreen() {
        StartCoroutine(changeRingAlpha(0, 1, LoopRingDelay, LoopImageList));
        ShineUnlock();
        mMoveScanerUp(ImgScaner, 1.5f);

    }

    void mHideText() {
        Debug.Log("隐藏文字");
        MovingText(UnlockScreenText, TextPosOffset, true, TextHideDelay);
    }

    void mShowText() {
        Debug.Log("显示文字");
        UnlockScreenText.text = UnlockText[1];
        MovingText(UnlockScreenText, TextPosOffset, false);
    }


    void MovingText(Text text,float Yoffset=0f, bool isHide = true, float delaytime = .5f,Action action = null) {
     Vector3     Texttarget = new Vector3(text.transform.localPosition.x, text.transform.localPosition.y - Yoffset, text.transform.localPosition.z);
         Vector3 pos = isHide? Texttarget : Textstartpos;

        LeanTween.moveLocal(text.gameObject, pos, .5f).setDelay(delaytime).setOnUpdate(delegate (float value)
        {//设置TEXT ALPHA从1-0；
         //      float alpha = -value;

            if (isHide)
            {
                float newalpha = UtilityFun.instance.Maping(value, 0, 1, 1, 0, false);
                ChangeTextAlpha(newalpha);
            }
            else {
           //     Debug.Log(value);
                ChangeTextAlpha(value);
            }
        }).setOnComplete(action);
        }



    void ChangeTextAlpha(float alpha) {
        UnlockScreenText.color = new Color(UnlockScreenText.color.r, UnlockScreenText.color.g, UnlockScreenText.color.b, alpha);
    }

    //开始时设置呼吸动画
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


        UtilityFun.instance.ChangeShineIntensity(ref ShineIntensity, 5f, .5f, ShineColor, ShineMaterialimg);
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
                //Debug.Log("当前LOOP值"+index / 2);


            });
                yield return new WaitForSeconds(time/2);
        }
    } 


   public IEnumerator countDown()
    {
        if (!inLock) {
            currentCountdownTime--;
            //Debug.Log(currentCountdownTime);
            yield return new WaitForSeconds(1);
            if (currentCountdownTime > 0)
            {

              StartCoroutine(  countDown());
            }
            else
            {
                minitilizeUnlockScreen();
            }
        }
    }
}
