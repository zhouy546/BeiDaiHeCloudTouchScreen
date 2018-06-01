using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoopRingCtr : Ctr {
    List<loopRing> loopRings;
	// Use this for initialization
	void Start () {
        initialization();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowAll() {
        foreach (var item in loopRings)
        {
            item.changeImageAlpha(1, 1, () => item.onDisplay = true);
        }
    }

    public void HideAll() {
        foreach (var item in loopRings)
        {
            item.changeImageAlpha(0, 1, () => item.onDisplay = false);
        }
    }

    public void initialization()
    {
        loopRing[] img = GetComponentsInChildren<loopRing>();

        loopRings = img.ToList();
    }
}
