using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class MainUI : MonoBehaviour {


    public Animation SemiCircleTrans;
    public List<NodeCtr> nodeCtrs = new List<NodeCtr>();

	// Use this for initialization
	void Start () {
        StartCoroutine(SetupMainUI());
    }


    IEnumerator SetupMainUI() {
        yield return new WaitForSeconds(.2f);
        foreach (var item in nodeCtrs)
        {
            item.SetUpNode();
        }

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            RotateSemicircle();
        }
	}


    void RotateSemicircle() {
        SemiCircleTrans.Play();
    }
}
