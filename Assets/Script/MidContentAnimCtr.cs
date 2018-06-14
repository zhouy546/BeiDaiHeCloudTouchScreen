using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidContentAnimCtr : MonoBehaviour {
    public NodeRayCastImg nodeRayCastImg;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NodeRayCastScaleUp()
    {
        nodeRayCastImg.ScaleUp();
    }

    public void NodeRayCastShink()
    {
        nodeRayCastImg.ShinkDown();
    }
}
