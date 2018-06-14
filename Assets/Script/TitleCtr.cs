using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCtr : Ctr {
    bool btitleNum=false;
   public TitleNum[] titleNums = new TitleNum[2];
    public TitleLine titleLine;
    public LoadImageCtr loadImageCtr;
    public TextGenerator textGenerator;
	// Use this for initialization
	void Start () {
    }	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    ShowAll();
        //}
        //else if (Input.GetKeyDown(KeyCode.S)) {
        //    HideAll();
        //}
	}

    public void HideAll() {
        HideNum();
        HideTitleLine();
        HideLoadImage();
        hideTextBlock();
    }

    public void ShowAll() {
        ShowNum();
        ShowTitleLine();
        showLoadImage();
        showTextBlock();
    }


    void showTextBlock() {
        textGenerator.ShowText(.2f);
    }

    void hideTextBlock() {
        textGenerator.HideText(.2f);
    }

    void showLoadImage() {
        loadImageCtr.ShowLoadImage();
    }

    void HideLoadImage()
    {
        loadImageCtr.HideLoadImage();
    }

    void HideNum() {

        StopAllCoroutines();
        for (int i = 0; i < titleNums.Length; i++)
        {
            titleNums[i].Hide();
        }
    }

    void HideTitleLine() {
        titleLine.Hide();
    }

    void ShowTitleLine() {
        titleLine.Show();
    }

    void ShowNum() {
        StartCoroutine(showNum());
    }

    IEnumerator showNum() {
        float time = Random.Range(.2f, 2f);

        btitleNum = !btitleNum;
        if (btitleNum)
        {
            titleNums[0].Show();
            titleNums[1].Hide();
        }
        else {
            titleNums[0].Hide();
            titleNums[1].Show();
        }
        yield return new WaitForSeconds(time);
        yield return StartCoroutine(showNum());
    }
}
