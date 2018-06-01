using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBG : MonoBehaviour {
  public  AnimationCurve curveRIGHT;
    public AnimationCurve curveLEFT;



    void Subscribe() {
        MainControler.MoveLeftEvent += moveLeft;
        MainControler.MoveRightEvent += moveRight;
    }

    void UnSubscribe() {
        MainControler.MoveLeftEvent -= moveLeft;
        MainControler.MoveRightEvent -= moveRight;
    }

    private void OnEnable()
    {
        Subscribe();
    }


    private void OnDisable()
    {
        UnSubscribe();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void moveLeft()
    {
        AddForce(8, curveRIGHT);
        LerpSpeed(10, 0.01f, 2f);
    }

    void moveRight() {
        AddForce(-8, curveLEFT);
        LerpSpeed(10, 0.01f, 2f);

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
