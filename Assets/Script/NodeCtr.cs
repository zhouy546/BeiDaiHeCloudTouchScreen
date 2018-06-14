using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NodeCtr : Ctr {

    public Animation SemiCircleTrans;

    public int Id=0;

    public bool IsInSpotlight= false;
    public bool isMoving;

    public int CurrenSlot;
    public List<Image> AllImage = new List<Image>();
    public LineSystem lineSystem;
    public SphereNodeCtr sphereNodeCtr;
    public DisplayObject displayObject;
    public DisplayObjectText displayObjectText;
    public Transform MidContent;
    public NodeRayCastImg nodeRayCastImg;
    public RectTransform BlockMask;


    //rotation value
    private Vector3 DisplayPos = new Vector3(-200,0,0);
    public Vector3 SlotPosition;
    public float vel;
    public float radiusX;
    public float radiusY;
    public float zRot = 45f;
    public float value;
    public float angle;

    public void SubScribe()
    {
        ScreenUnlocker.EndUnlockScreenEvent += mShowMainDisplay;
    }


    public void Unscribe()
    {
        ScreenUnlocker.EndUnlockScreenEvent -= mShowMainDisplay;


    }

    private void OnEnable()
    {
        SubScribe();
    }

    private void OnDisable()
    {
        Unscribe();
    }



    // Use this for initialization
    void Start () {
      StartCoroutine(  initialization());
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Debug.Log("SHINKDown");
        //    shinkDown();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    Debug.Log("scale up");
        //    ScaleUp();
        //}
    }
    public void HideDisplayObject(float time)
    {
        displayObject.SetObjectAlpha(displayObject.meshRenderer, 0, time);

    }

    public void ShowDisplayObject(float time) {
        displayObject.SetObjectAlpha(displayObject.meshRenderer, 1, time);

    }

    public void SetUpNode() {
        AllImage = UtilityFun.instance.GetDisplayImage(this.gameObject);
        SetAllImageAlpha(AllImage, 0f, 0f);
        sphereNodeCtr.DimAllSpheres();
        lineSystem.setLineAlpha(0, 0);
        HideDisplayObject(0);
    }




    void mShowMainDisplay()
    {
        if (IsInSpotlight) {
            SetAllImageAlpha(AllImage, 1f, .2f);
            displayObjectText.ShowText(1);
         //   lineSystem.SetObjectAlpha(lineSystem.LineSystemMeshRender, 1, .2f);
            lineSystem.setLineAlpha(1, .2f);
            sphereNodeCtr.GlowAllSpheres();
            ShowDisplayObject(.2f);
            Debug.Log("show mainDisplay");
        }
    }

  public  void SetAllImageAlpha(List<Image> images, float Alpha, float time)
    {
        foreach (var item in images)
        {
            LeanTween.value(item.color.a, Alpha, time).setOnUpdate(delegate (float value)
            {
                Color tempColor = new Color(item.color.r, item.color.g, item.color.b, value);

                item.color = tempColor;
            });

        }
    }

    void RotateSemicircle()
    {
        SemiCircleTrans.Play("SemiCircleAnimation");
    }


    public void DimAll() {
        HideText();
        DimLine();
        DimSphere();
        // shinkDown();
        HideDisplayObject(.2f);
        nodeRayCastImg.HideImage();
        HideBlockMask();

        displayObject.SetObjectAlpha(displayObject.meshRenderer, 0, .2f);
    }

    public void DimLine() {
        lineSystem.setLineAlpha(0, 1);
    }

    public void DimSphere()
    {
        sphereNodeCtr.DimAllSpheres();
    }

    public void GlowAll() {
        ShowText();
        GlowLine();
        GlowSpherel();
        //ScaleUp();
        nodeRayCastImg.ShowImage();
        //   nodeRayCastImg.ShowAnimation();
        ShowBlockMask();
        ShowDisplayObject(.2f);

        displayObject.SetObjectAlpha(displayObject.meshRenderer, 1, .2f);
    }

    public void HideBlockMask() {
        BlockMask.gameObject.SetActive(false);
    }

    public void ShowBlockMask() {
        BlockMask.gameObject.SetActive(true);
    }

    public void GlowLine()
    {
        lineSystem.setLineAlpha(1, 1);
    }

    public void GlowSpherel()
    {
        sphereNodeCtr.GlowAllSpheres();
    }

    public void ShowText() {
        displayObjectText.ShowText(1f);
    }

    public void HideText() {
        displayObjectText.HideText(1f);
    }


    public void shinkDown()
    {
        if (IsInSpotlight) {
            foreach (var item in AllImage)
            {
                if (item.GetComponent<Animation>() != null)
                {
                    item.GetComponent<Animation>().Play("CircleShinkDown");
                }
            }
            MidContent.GetComponent<Animation>().Play("DisplayObjectShinkDown");
        }
        IsInSpotlight = false;
    }





    public void ScaleUp() {
        if (!IsInSpotlight) {
            foreach (var item in AllImage)
            {
                if (item.GetComponent<Animation>() != null)
                {
                    item.GetComponent<Animation>().Play("CircleScaleUp");
                }
            }
            MidContent.GetComponent<Animation>().Play("DisplayObjectScaleUp");
        }
        IsInSpotlight = true;
    }



    public void MoveToLocation(Vector3 pos) {
        LeanTween.moveLocal(this.gameObject, pos, 1f).setEase(LeanTweenType.easeInOutQuad);
    }

    public void MoveToTargetEngle(float targetAngel) {
        if (!isMoving) {

            isMoving = true;
            float temp = targetAngel;

            if (angle - targetAngel > 180)
            {
                temp = temp + 360;
            }
            else if (angle - targetAngel < -180)
            {
                temp = temp - 360;
            }

            LeanTween.value(angle, temp, 1f).setEase(LeanTweenType.easeInOutQuad).setOnUpdate(delegate (float value)
            {
                Movement(value);
            }).setOnComplete(delegate () {


                angle = targetAngel;
                isMoving = false;

            });
        }

    }


    public void  Movement(float _angle)
    {
        float rad = Mathf.Deg2Rad * _angle;
        float radx = Mathf.Deg2Rad * (_angle + value);

        float x = radiusX * Mathf.Sin(radx)*Mathf.Sin(Mathf.Deg2Rad * zRot);
//        Debug.Log(1 / Mathf.Sin(Mathf.Deg2Rad * zRot));

        float y = radiusY * Mathf.Cos(rad ) /(1 / Mathf.Sin(Mathf.Deg2Rad * zRot));
        Vector3 pos = new Vector3(x, y, 0);
      //  Debug.Log(pos);

        this.transform.localPosition = pos;
        SlotPosition = pos;
    }

    public void MoveToSoloPosition() {
        Debug.Log("移动到Solo 位置");
        MoveTo(this.gameObject,DisplayPos, 1f);
    }

    public void MoveToSlotPosition(Action action=null)
    {
        MoveTo(this.gameObject,SlotPosition, 1f, action);
    }

    public void TurnOnInteraction() {
        nodeRayCastImg.setRayCastTarget(true);
    }

    public void TurnOffInteraction()
    {
        nodeRayCastImg.setRayCastTarget(false);
    }

    //public void ToggleToSoloDisplayPos(bool b)
    //{
    //    if (b)
    //    {

    //    }
    //    else {
    //  //      MoveToTargetEngle(MainUI.instance.NodeSlotAngle[CurrenSlot]);
    //    }
    //}



    IEnumerator initialization() {
        yield return new WaitForSeconds(.2f);
        SetUpNode();

        CurrenSlot =Id;
        IsInSpotlight = true;
        isMoving = false;
        MoveToTargetEngle(MainUI.instance.NodeSlotAngle[CurrenSlot]);
    }

}
