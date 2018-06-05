using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSystem : MonoBehaviour {
    public SphereNodeCtr sphereNodeCtr;

    public List<Transform> Nodes;
    public LineRenderer lineRenderer;

    public List<Transform> DrawNodePos;
    public int lengthOfLineRenderer;
    // Use this for initialization

    void Start () {

        initialization();
    }

    public void initialization()
    {
        Nodes = GetSphereNodesTrans();

        lengthOfLineRenderer = Nodes.Count * 2;
        lineRenderer.material = new Material(Shader.Find("Glow 11/Unity/Transparent/Cutout/Bumped Diffuse"));
        lineRenderer.widthMultiplier = 0.03f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        DrawNodePos = NodePicker(Nodes, Nodes.Count - 1);

        RotateMySelf();
    }


    List<Transform> GetSphereNodesTrans() {
        List<MeshRenderer> tempRender = UtilityFun.instance.GetMeshRenders(this.gameObject);
        List<Transform> temp = new List<Transform>();

        foreach (var item in tempRender)
        {
            temp.Add(item.GetComponent<Transform>());
        }
        return temp;
    }

    void RotateMySelf() {
        this.GetComponent<Animation>().Play();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Nodes.Count; i++)
        {
            lineRenderer.SetPosition(i, Nodes[i].position);
        }
        for (int i = 1; i < Nodes.Count; i++)
        {
            lineRenderer.SetPosition(Nodes.Count + i, DrawNodePos[i].position);
        }
        lineRenderer.SetPosition(Nodes.Count, Nodes[0].position);
    }

    List<Transform> NodePicker(List<Transform> itemList , int endNum) {
        List<int> temp = new List<int>();
        List < Transform > transTemp = new List<Transform>();
        for (int i = 0; i < endNum+1; i++)
        {
            temp.Add(i);
        }

        while (temp.Count!=0)
        {
          int randomValue =   Random.Range(0, endNum+1);

            while (temp.Contains(randomValue))
            {
               // Debug.Log(randomValue);
                temp.Remove(randomValue);
                transTemp.Add( itemList[randomValue]);
            }
        }
        return transTemp;
    }


    public void SetObjectAlpha(List<MeshRenderer> MeshRenderer, float intensity, float time)
    {
        foreach (var item in MeshRenderer)
        {
            //Debug.Log(item.gameObject.name);
            LeanTween.value(item.material.GetFloat("_GlowStrength"), intensity, time).setOnUpdate(delegate (float value)
            {
                item.material.SetFloat("_GlowStrength", value);
            });
        }
    }

    public void setLineAlpha(float alpha, float time) {

        //set color
        LeanTween.value(lineRenderer.material.color.a, alpha, time).setOnUpdate(delegate (float value)
        {
            lineRenderer.material.color = new Color(lineRenderer.material.color.r, lineRenderer.material.color.g, lineRenderer.material.color.b, alpha);
        });
        //set intensity
        LeanTween.value(lineRenderer.material.GetFloat("_GlowStrength"), alpha, time).setOnUpdate(delegate (float value)
        {
            lineRenderer.material.SetFloat("_GlowStrength", value);
        });
    }

}




