using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class MainUI : MonoBehaviour {


    public static MainUI instance;
  //  public loopRing LoopRing;

   // public RightBottomDescription rightBottomDescription;

    public Animation SemiCircleTrans;
    public List<NodeCtr> nodeCtrs = new List<NodeCtr>();
    public RightBottomDescriptionCtr rightBottomDescriptionCtr;
    public LoopRingCtr loopRingCtr;
    public List<float> NodeSlotAngle;

   public void SubScribe() {
        MainControler.MoveLeftEvent += MoveLeft;
        MainControler.MoveRightEvent += MoveRight;
    }

 public   void UnSubscribe() {
        MainControler.MoveLeftEvent -= MoveLeft;
        MainControler.MoveRightEvent -= MoveRight;
    }


    private void OnEnable()
    {
    //    SubScribe();
    }

    private void OnDisable()
    {
       UnSubscribe();
    }

    // Use this for initialization
    void Start () {
        if (instance == null) {
            instance = this;
        }

    }





   public IEnumerator SetupMainUI() {
        yield return new WaitForSeconds(.2f);
        //设置Node
        foreach (var item in nodeCtrs)
        {
            item.SetUpNode();
        }

        //2018/5/30~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //设置DescrptionCircle

        //设置LoopCircle

        loopRingCtr.HideAll();
    }




    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{

        //    RotateSemicircle();
        //}
    }
    //向左移动
   public void MoveLeft() {
        MainControler.instance.SetState(AppState.Loop,Left);
    }

    public void Left() {
        foreach (var item in nodeCtrs)
        {
            if (!item.isMoving)
            {

                float temp = NodeSlotAngle[LoopListDecrease(NodeSlotAngle, ref item.CurrenSlot)];

                item.MoveToTargetEngle(temp);
            }
        }
    }

    //向右移动
   public void MoveRight() {
        MainControler.instance.SetState(AppState.Loop,Right);

    }

    public void Right() {

        foreach (var item in nodeCtrs)
        {
            if (!item.isMoving)
            {

                float temp = NodeSlotAngle[LoopListIncrease(NodeSlotAngle, ref item.CurrenSlot)];

                item.MoveToTargetEngle(temp);
            }
        }
    }

    public void LockHideAllNodes()
    {
        foreach (var item in nodeCtrs)
        {
            Debug.Log("HideAllNode");
            //          item.HideText();
            item.DimAll();
            item.SetAllImageAlpha(item.AllImage, 0f, 0f);
        }
        loopRingCtr.HideAll();


        HideRightBottomDescription();
        ToggleRightBottomDescriptionPlayAnim(true);
    }


    public void ResttoLoop() {
        if (true)
        {
            loopRingCtr.ShowAll();
            foreach (var item in nodeCtrs)
            {
             //   item.nodeRayCastImg.ShowImage();
              //  item.ShowDisplayObject(.2f);
                item.shinkDown();
                item.SetAllImageAlpha(item.AllImage, 1f, .2f);

                item.GlowAll();
            }

            HideRightBottomDescription();
            ToggleRightBottomDescriptionPlayAnim(true);
        }
    }





    public void ResttoSolo()
    {
        if (true)
        {
            TurnOffAllNodeInteraction();
            TurnOnOneNodeInteraction(MainControler.instance.temp.nodeCtr);
            MainControler.instance.temp.nodeCtr.GlowAll();
            MainControler.instance.temp.nodeCtr.ScaleUp();
            loopRingCtr.HideAll();
            ShowRightBottomDescription();
            ToggleRightBottomDescriptionPlayAnim(false);
        }
    }
    public void HideRightBottomDescription()
    {
        rightBottomDescriptionCtr.HideAll();
    }

    public void ShowRightBottomDescription()
    {
        rightBottomDescriptionCtr.ShowAll();
    }

    public void ToggleRightBottomDescriptionPlayAnim(bool b)
    {
        if (b)
        {
            rightBottomDescriptionCtr.showMask();
        }
        else
        {
            rightBottomDescriptionCtr.HideMask();
        }
    }

    //循环LIST[] 里面的数值 向上增大
    int LoopListIncrease(List<float> Anglelist,  ref int Num) {
       int  temp = Num+1;
        if (temp >= Anglelist.Count) {
            Num = 0;

            return 0;
        }
        Num = temp;

        return temp;
    }
    //循环LIST[] 里面的数值 向下减小

    int LoopListDecrease(List<float> Anglelist, ref int Num)
    {
        int temp = Num - 1;
        if (temp < 0)
        {
            Num = Anglelist.Count - 1;
            return Anglelist.Count-1;
        }
        Num = temp;

        return temp;
    }


    public void TurnOnAllNodeInteraction() {
        foreach (var item in nodeCtrs)
        {
            item.TurnOnInteraction();
        }
    }

    public void TurnOffAllNodeInteraction() {
        foreach (var item in nodeCtrs)
        {
            item.TurnOffInteraction();
        }
    }

    public void TurnOnOneNodeInteraction(NodeCtr _nodeCtr) {
        foreach (var item in nodeCtrs)
        {
            if (item.Id == _nodeCtr.Id)
            {
                item.TurnOnInteraction();
            }
        }
    }

    public void TurnOffOneNodeInteraction(NodeCtr _nodeCtr)
    {
        foreach (var item in nodeCtrs)
        {
            if (item.Id == _nodeCtr.Id)
            {
                item.TurnOffInteraction();
            }
        }
    }
}
