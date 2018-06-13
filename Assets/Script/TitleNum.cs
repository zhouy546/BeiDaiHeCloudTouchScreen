using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleNum : UITextBase {
 
    // Use this for initialization
    void Start () {
        base.Start();

    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    public void Hide() {
        HideText(.125f);
    }

    public void Show() {
        ShowText(.125f);
        changeText();
    }

    public void GenerateText() {
        changeText();
    }


    public void changeText() {

        string dacade = Mathf.FloorToInt(Random.Range(0f, 2f)).ToString();
        string unitys = Mathf.FloorToInt(Random.Range(0f, 9f)).ToString();
        string tempText = dacade + unitys;
        text.text = tempText;
    }
}
