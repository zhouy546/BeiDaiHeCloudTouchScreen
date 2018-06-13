using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeRayCastImg : UIImageBase {
    public NodeCtr nodeCtr;
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
        HideImage();
        setRayCastTarget(false);
    }

  public  void setRayCastTarget(bool b) {
        image.raycastTarget = b;
    }

    public void ShowAnimation() {
        this.transform.localScale = 1.1f * Vector3.one;
        LeanTween.scale(this.gameObject, Vector3.one, .5f).setEase(LeanTweenType.easeOutQuad).setDelay(1.2f);
    }
}
