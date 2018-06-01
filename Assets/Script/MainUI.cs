using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class MainUI : MonoBehaviour {

  //  public loopRing LoopRing;

   // public RightBottomDescription rightBottomDescription;

    public Animation SemiCircleTrans;
    public List<NodeCtr> nodeCtrs = new List<NodeCtr>();
    public RightBottomDescriptionCtr rightBottomDescriptionCtr;
    public LoopRingCtr loopRingCtr;
    public List<float> NodeSlotAngle;

    void SubScribe() {
        MainControler.MoveLeftEvent += MoveLeft;
        MainControler.MoveRightEvent += MoveRight;
    }

    void UnSubscribe() {
        MainControler.MoveLeftEvent -= MoveLeft;
        MainControler.MoveRightEvent -= MoveRight;
    }


    private void OnEnable()
    {
        SubScribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(SetupMainUI());
    }





    IEnumerator SetupMainUI() {
        yield return new WaitForSeconds(.2f);
        //设置Node
        foreach (var item in nodeCtrs)
        {
            item.SetUpNode();
        }

        //2018/5/30~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //设置DescrptionCircle

        //设置LoopCircle
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            RotateSemicircle();
        }
    }
    //向左移动
    void MoveLeft() {
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
    void MoveRight() {
        foreach (var item in nodeCtrs)
        {
            if (!item.isMoving)
            {

                float temp = NodeSlotAngle[LoopListIncrease(NodeSlotAngle, ref item.CurrenSlot)];

                item.MoveToTargetEngle(temp);
            }
        }
    }



    void RotateSemicircle() {
        SemiCircleTrans.Play("SemiCircleAnimation");
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
}
