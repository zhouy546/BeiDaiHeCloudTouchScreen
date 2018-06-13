using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadImageCtr : Ctr {
    public List<LoadImage> loadImages = new List<LoadImage>();
	// Use this for initialization
	void Start () {
        base.Start();
        loadImages = GetLoadImages();
        DisableRandomBlink();
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }

    public void HideLoadImage() {
        DisableRandomBlink();
    }

    public void ShowLoadImage() {
        EnabelRandomBlink();
    }

    List<LoadImage> GetLoadImages() {
        LoadImage[] images=  this.GetComponentsInChildren<LoadImage>();
        return images.ToList();
    }

    public void EnabelRandomBlink()
    {
        foreach (var loadImage in loadImages)
        {
            loadImage.StartBlink();
        }
    }

    public void DisableRandomBlink()
    {
        foreach (var loadImage in loadImages)
        {
            loadImage.StopBlink();
        }
    }
}
