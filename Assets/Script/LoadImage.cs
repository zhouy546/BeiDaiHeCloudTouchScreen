using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadImage : UIImageBase {

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    public void StartBlink() {
       float alpha = Random.Range(0f, 1f);
        float time = Random.Range(0f, 0.5f);
        changeImageAlpha(alpha, time, StartBlinkEnd);
       // Debug.Log("startBlink");
    }



    public void StopBlink() {
        if (alphaLTDescr != null) {
            cancelAlphaTween();
        }
        changeImageAlpha(0, .5f, StopBlinkEnd);
    }

    public  void StartBlinkEnd()
    {
        StartBlink();
    }

    public void StopBlinkEnd()
    {

    }
}
