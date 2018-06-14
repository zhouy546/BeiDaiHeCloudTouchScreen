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

    public void ScaleUp() {
        LeanTween.scale(this.gameObject, 1.2f*Vector3.one, .5f).setEase(LeanTweenType.easeOutBounce);
    }

    public void ShinkDown() {
        LeanTween.scale(this.gameObject, Vector3.one, .5f).setEase(LeanTweenType.easeInBounce);
    }
}
