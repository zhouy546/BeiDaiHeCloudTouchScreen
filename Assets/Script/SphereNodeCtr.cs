using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereNodeCtr : Ctr {
  public   List<SphereNode> sphereNodes;
	// Use this for initialization
	void Start () {
        initialization();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DimAllSpheres() {
        foreach (var item in sphereNodes)
        {
            item.ObjectDim();
        }
    }

    public void GlowAllSpheres() {
        foreach (var item in sphereNodes)
        {
            item.ObjectGlow();
        }
    }

    public void initialization()
    {
        SphereNode[] sphere = GetComponentsInChildren<SphereNode>();

        sphereNodes = sphere.ToList();
        //2018/6/1-----------------
        DimAllSpheres();
    }
}
