using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum AppState
{
    initialization,
    Looker, Solo, Loop
}

public class MainControler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IDragHandler, IEndDragHandler, IPointerUpHandler
{

    #region mouseCTR
    public delegate float MoveDistanceDelegate();

    public enum Move
    {
        MoveRight, MoveLeft, Idle
    }
    public static bool isUpDownDirection = false;

    struct ClientInfo
    {
        public Vector2 StartPos;
        public Vector2 UpdatePos;
        public Vector2 EndPos;
        private float movedis;
        public float MoveDistance
        {
            get
            {
                if (MainControler.isUpDownDirection)
                {
                    return movedis = Mathf.Abs(EndPos.y - StartPos.y);
                }
                else
                {
                    return movedis = Mathf.Abs(EndPos.x - StartPos.x);
                }

            }
        }
    }

    ClientInfo client;
    #endregion



    public MainUI mainUI;
    public ParticleBG particleBG;
    public ScreenUnlocker screenUnlocker;


    public static MainControler instance;

    public AppState appState = AppState.Looker;

    public AppState PerviousState ;

    public delegate void MoveNode();
    public static event MoveNode MoveLeftEvent;
    public static event MoveNode MoveRightEvent;

    public NodeRayCastImg temp;
    private bool ismove;

   public void SetState(AppState _appState,Action action=null) {
        switch (_appState)
        {
            case AppState.initialization:
      
                appState = AppState.initialization;

                break;

            case AppState.Looker:

                if (appState != AppState.Looker) {
                    PerviousState = appState;
                    appState = AppState.Looker;
                    Lock();
                }
                break;

            case AppState.Solo:
                if (appState != AppState.Solo)
                {
                    PerviousState = appState;
                    appState = AppState.Solo;
                    solo();

                }
                break;

            case AppState.Loop:

                if (appState != AppState.Loop)
                {
                    Loop(action);
                    PerviousState = appState;
                    appState = AppState.Loop;

                }
                else {
                    Debug.Log("normal move");
                    if (action != null)
                    {
                        action();

                    }
                }
                break;
        }
    }

    private void Lock()
    {
        mainUI.LockHideAllNodes();
        if (temp != null) {
            temp.nodeCtr.IsInSpotlight = false;
        }
        turnoffMovement();
    }

    private void Loop(Action action) {
        if (appState != AppState.Loop)
        {
            mainUI.ResttoLoop();//重新设置所有
            if (temp != null)
            {
                Debug.Log("doing!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1");

                temp.nodeCtr.MoveToSlotPosition(action);//先回原位，再移动slot

                temp.nodeCtr.IsInSpotlight = false;
            }
        }
    }

    private void solo() {
        mainUI.ResttoSolo();

        temp.nodeCtr.MoveToSoloPosition();
        if (temp.nodeCtr.IsInSpotlight)
        {
            temp.nodeCtr.IsInSpotlight = true;
        }

        Debug.Log("to Solo");
    }

    public void turnoffMovement() {
        mainUI.UnSubscribe();
    }

    public void turnonMovement() {
        mainUI.SubScribe();
    }

    public void mMoveLeftEvent()
    {
        Debug.Log("MoveLeft");

        if (MoveLeftEvent != null)
        {
            MoveLeftEvent();
        }
    }

    public void mMoveRightEvent()
    {
        Debug.Log("MoveRight");

        if (MoveRightEvent != null)
        {
            MoveRightEvent();
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        client.StartPos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");


        client.EndPos = eventData.position;
        ismove = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        client.UpdatePos = eventData.position;
        client.EndPos = eventData.position;

        if (isUpDownDirection)
        {
            MoveBehavior(MoveDirection(yAxisDis));

        }
        else
        {
            MoveBehavior(MoveDirection(xAxisDis));
        }


        Debug.Log("OnDrag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //重置屏保时间 

        screenUnlocker.currentCountdownTime = screenUnlocker.countDownTime;

        string str = eventData.pointerCurrentRaycast.gameObject.name;
     //   Debug.Log(str);
        if (str == "UnlockTouchPad") {
            //解锁
            screenUnlocker.mBeginUnlockScreenEvent();

            //关闭全部Node交互
            mainUI.TurnOffAllNodeInteraction();

            //解锁完成后被全部打开

            eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().raycastTarget = false;


        }

        if (str == "NodeRayCastImg")
        {
            //显示SOLO模式
             temp = eventData.pointerCurrentRaycast.gameObject.GetComponent<NodeRayCastImg>();
            SetState(AppState.Solo);


        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");

    }

    public void MoveBehavior(Move move)
    {
        switch (move)
        {
            case Move.MoveRight:

            //    SetState(AppState.Loop);
                mMoveRightEvent();

                break;
            case Move.MoveLeft:
           //    SetState(AppState.Loop);
                mMoveLeftEvent();
                break;
            case Move.Idle:

                break;
            default:
                break;
        }
    }

    public Move MoveDirection(MoveDistanceDelegate moveDistanceDelegate)
    {
        float dis = moveDistanceDelegate();
        if (dis > 0 && client.MoveDistance > 100&& !ismove)
        {
            //Debug.Log("move down");
            client.StartPos = client.EndPos;
            ismove = true;
            return Move.MoveLeft;
        }
        else if (dis < 0 && client.MoveDistance > 100&&!ismove)
        {
            //Debug.Log("move up");
            client.StartPos = client.EndPos;
            ismove = true;
            return Move.MoveRight;
        }
        return Move.Idle;
    }

    float yAxisDis()
    {
        return client.EndPos.y - client.StartPos.y;
    }

    float xAxisDis()
    {
        return client.EndPos.x - client.StartPos.x;
    }

    // Use this for initialization
    void Start () {

        if (instance == null) {
            instance = this;
        }

        SetState(AppState.Looker);

    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.A))
        //{

        //    mMoveLeftEvent();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{

        //    mMoveRightEvent();
        //}
    }



}
