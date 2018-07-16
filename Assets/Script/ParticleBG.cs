using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBG : MonoBehaviour {
  public  AnimationCurve curveRIGHT;
    public AnimationCurve curveLEFT;
    public AnimationCurve IdleCurve;



   public void Subscribe() {
        MainControler.MoveLeftEvent += moveLeft;
        MainControler.MoveRightEvent += moveRight;
    }

   public void UnSubscribe() {
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
    //    LerpSpeed(10, 0.01f, 2f);
    }

    void moveRight() {
        AddForce(-8, curveLEFT);
       // LerpSpeed(10, 0.01f, 2f);

    }


    void AddForce(float force, AnimationCurve curve) {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var ex = ps.velocityOverLifetime;
        ex.enabled = true;
        ex.x = new ParticleSystem.MinMaxCurve(force,curve);
       LerpSpeed(2, 4, .5f);
    }


    void LerpSpeed(float from,float to,float time) {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var lm = ps.limitVelocityOverLifetime;

        LeanTween.value(from, to, time).setOnUpdate(delegate (float value)
        {
            lm.limitX = value;
        }).setOnComplete(delegate () {
            LeanTween.value(to, .1f, time).setOnUpdate(delegate (float val)
            {
                lm.limitX = val;
            }).setOnComplete(delegate () {
                var ex = ps.velocityOverLifetime;
                ex.x = new ParticleSystem.MinMaxCurve(1, IdleCurve);
            });
        });
    }
}
