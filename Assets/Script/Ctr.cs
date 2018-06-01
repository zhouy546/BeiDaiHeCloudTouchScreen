using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctr : MonoBehaviour {
    public AnimationCurve Scalecurve;
    public float scaleTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 设置该物体的大小
    /// </summary>
    /// <param name="size"></param>
    /// <param name="time"></param>
    public virtual void SetObjectSize(Vector3 size, float time,AnimationCurve curve)
    {
        LeanTween.scale(this.gameObject, size, time).setEase(curve);
    }
}
