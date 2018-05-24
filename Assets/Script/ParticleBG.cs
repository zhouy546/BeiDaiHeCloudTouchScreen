using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBG : MonoBehaviour {
  public  AnimationCurve curveRIGHT;
    public AnimationCurve curveLEFT;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            AddForce(8, curveRIGHT);
            LerpSpeed(10, 0.01f, 2f);
        }
        else if(Input.GetMouseButtonDown(1)) {
            AddForce(-8, curveLEFT);
            LerpSpeed(10, 0.01f, 2f);
        }
    }


    void AddForce(float force, AnimationCurve curve) {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var ex = ps.forceOverLifetime;
        ex.enabled = true;
        ex.x = new ParticleSystem.MinMaxCurve(force, curve);
        LerpSpeed(0.01f, 10, .2f);
    }


    void LerpSpeed(float from,float to,float time) {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var lm = ps.limitVelocityOverLifetime;
        LeanTween.value(from, to, time).setOnUpdate(delegate (float value) {
            lm.limitX = value;
        });
    }
}
