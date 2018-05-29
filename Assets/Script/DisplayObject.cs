using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObject : MonoBehaviour {
  public   MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}


public    void SetObjectAlpha(MeshRenderer meshRenderer, float Alpha, float time)
    {

            LeanTween.value(meshRenderer.material.color.a, Alpha, time).setOnUpdate(delegate (float value)
            {
                Color tempColor = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, value);

                meshRenderer.material.color = tempColor;
            });

    }
}
