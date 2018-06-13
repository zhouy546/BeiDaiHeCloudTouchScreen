using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeftSubTitleCtr : Ctr {
    List<TitleCtr> titleCtrs = new List<TitleCtr>();
	// Use this for initialization
	void Start () {
        base.Start();
        getTitleCtr();

    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    void getTitleCtr() {
        titleCtrs = this.GetComponentsInChildren<TitleCtr>().ToList();
    }

    public void ShowAll()
    {
        foreach (var titleCtr in titleCtrs)
        {
            titleCtr.ShowAll();
        }
    }

    public void HideAll()
    {
        foreach (var titleCtr in titleCtrs)
        {
            titleCtr.HideAll();
        }
    }
}
