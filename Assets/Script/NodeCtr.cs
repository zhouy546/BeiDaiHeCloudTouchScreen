using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NodeCtr : MonoBehaviour {
    public List<Image> AllImage = new List<Image>();
    public LineSystem lineSystem;
    public DisplayObject displayObject;



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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


   public void SetUpNode() {
        AllImage = UtilityFun.instance.GetDisplayImage(this.gameObject);
        SetAllImageAlpha(AllImage, 0f, 0f);
        lineSystem.SetObjectAlpha(lineSystem.LineSystemMeshRender, 0, 0);
        lineSystem.setLineAlpha(0, 0);
        displayObject.SetObjectAlpha(displayObject.meshRenderer, 0, 0);
    }



    void mShowMainDisplay()
    {
        SetAllImageAlpha(AllImage, 1f, .2f);
        lineSystem.SetObjectAlpha(lineSystem.LineSystemMeshRender, 1, .2f);
        lineSystem.setLineAlpha(1, .2f);
        displayObject.SetObjectAlpha(displayObject.meshRenderer, 1, .2f);
        Debug.Log("show mainDisplay");
    }

    void SetAllImageAlpha(List<Image> images, float Alpha, float time)
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

}
