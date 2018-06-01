using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainControler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler
{

  public  MainUI mainUI;


    public enum AppState { initialization,
        LockToHeighlightState, LockToLoopState,
        LoopToHighlightState,LoopToLockState,
        HighlightToLoopState, HightlightToLockState,
         }

    public AppState appState;
    public delegate void MoveNode();
    public static event MoveNode MoveLeftEvent;
    public static event MoveNode MoveRightEvent;


    void SetState(AppState _appState) {
        switch (_appState)
        {
            case AppState.initialization:
                appState = AppState.initialization;

                break;

            case AppState.LockToHeighlightState:
                ScreenUnlocker.instance.mBeginUnlockScreenEvent();
                break;

            case AppState.LockToLoopState:

                break;

            case AppState.LoopToHighlightState:

                break;
            case AppState.LoopToLockState:

                break;
            case AppState.HighlightToLoopState:

                break;
            case AppState.HightlightToLockState:

                break;
        }
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {

            mMoveLeftEvent();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            mMoveRightEvent();
        }
    }
}
