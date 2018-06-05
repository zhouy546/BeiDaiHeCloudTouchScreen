using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayObjectText : UITextBase {

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
        text = this.GetComponent<Text>();
        CurrentColor = Defaultcolor = text.color;
        HideText(0);
    }
}
