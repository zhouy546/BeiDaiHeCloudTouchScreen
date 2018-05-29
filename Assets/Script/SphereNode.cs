using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereNode : MonoBehaviour {

    public struct Node {
        public Vector3 pos;
        public float Vel;
        public float radx;
        public float rady;
            public float Force;
        public float LightIntensity;
    }

    Node node = new Node();

    private void Start()
    {
        float vel = Random.Range(.5f, 2f);
        float radx = Random.Range(.2f, 1f);
        float rady = Random.Range(.2f, 1f);
        InitializedNode(this.transform.localPosition, vel, radx,rady, 1);
    }

    private void Update()
    {
        Movement();
    }


    void InitializedNode(Vector3 pos,float vel,float radx,float rady,float LightIntensity) {
        node.radx = radx;
        node.rady = rady;

        node.pos = pos;
        node.Vel = vel;
        node.LightIntensity = LightIntensity;
    }

    void Movement() {
        float x = node.radx * Mathf.Sin(Time.time * node.Vel) + node.pos.x;
        float y =node.rady * Mathf.Cos(Time.time * node.Vel) + node.pos.y;
    //    Debug.Log(x);
        Vector3 pos = new Vector3(x, y, node.pos.z);
        node.pos = pos;
 this.transform.localPosition = node.pos;
    }

}
