using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RightBottomDescriptionCtr : Ctr
{

    public float ImageMaskDelay;
    List<ImageMask> imageMasks;
   List< RightBottomDescriptionImg> rightBottomDescriptionImgs;
  List<  RightBottomDesctiptionText> rightBottomDesctiptionTexts;
    // Use this for initialization
    void Start () {
        initialization();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)) {
            SetObjectSize(Vector3.one, scaleTime, Scalecurve);
            ShowAll();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetObjectSize(Vector3.zero, scaleTime, Scalecurve);
            HideAll();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
           StartCoroutine(ShowImageMask(ImageMaskDelay));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(HideImageMask(ImageMaskDelay));
        }
    }

    /// <summary>
    /// 显示白色Mask
    /// </summary>
    /// <param name="delay 延后时间"></param>
    /// <returns></returns>
  public  IEnumerator ShowImageMask(float delay)
    {
        foreach (var item in imageMasks)
        {
            item.ShowImageMask();
          //  item.ShowImage(1, 1);
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// 隐藏白色Mask
    /// </summary>
    /// <param name="delay 延后时间"></param>
    /// <returns></returns>
    public IEnumerator HideImageMask(float delay)
    {
        foreach (var item in imageMasks)
        {
            item.HideImageMask();
            //item.HideImage(0,1);
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// 重置所有子物体
    /// </summary>
    public void ResetAll()
    {
        ResetMask();
        ResetImg();
        ResetText();
    }

    public void ResetMask()
    {
        foreach (var item in imageMasks)
        {
            item.ResetItem();
        }
    }

    public void ResetImg()
    {
        foreach (var item in rightBottomDescriptionImgs)
        {
            item.ResetItem();
        }
    }

    public void ResetText()
    {
        foreach (var item in rightBottomDesctiptionTexts)
        {
            item.ResetItem();
        }
    }

    /// <summary>
    /// 显示该物体下所有
    /// </summary>
    public void HideAll() {

        HideAllImage();
        HideAllText();

    }
    /// <summary>
    /// 隐藏该物体下所有
    /// </summary>
    public void ShowAll() {
        ShowAllImage();
        ShowAllText();
    }
    /// <summary>
    /// 显示所有图片
    /// </summary>
    public void ShowAllImage()
    {
        foreach (var item in rightBottomDescriptionImgs)
        {
            item.changeImageAlpha(1, .2f, () => item.onDisplay = false);
        }

        foreach (var item in imageMasks)
        {
            item.changeImageAlpha(1,.2f, () => item.onDisplay = false);
        }
    }
    /// <summary>
    /// 隐藏所有图片
    /// </summary>
    public void HideAllImage()
    {
        foreach (var item in rightBottomDescriptionImgs)
        {
            item.changeImageAlpha(0,.2f,()=>item.onDisplay=false);
        }

        foreach (var item in imageMasks)
        {
            item.changeImageAlpha(0, .2f, () => item.onDisplay = false);
        }
    }
    /// <summary>
    /// 显示文字
    /// </summary>
    public void ShowAllText() {
        foreach (var item in rightBottomDesctiptionTexts)
        {
            item.ShowText(1, 1);
        }
    }
    /// <summary>
    /// 隐藏文字
    /// </summary>
    public void HideAllText() {
        foreach (var item in rightBottomDesctiptionTexts)
        {
            item.HideText(0, 0);
        }
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void initialization() {

        RightBottomDescriptionImg[] img = GetComponentsInChildren<RightBottomDescriptionImg>();

        rightBottomDescriptionImgs = img.ToList();

        RightBottomDesctiptionText[] Text = GetComponentsInChildren<RightBottomDesctiptionText>();

        rightBottomDesctiptionTexts = Text.ToList();

        ImageMask[] mask = GetComponentsInChildren<ImageMask>();

        imageMasks = mask.ToList();

        SetObjectSize(Vector3.zero, 0, Scalecurve);
    }

}
