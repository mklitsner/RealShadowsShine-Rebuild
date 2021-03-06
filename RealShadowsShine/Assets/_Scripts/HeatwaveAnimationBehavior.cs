﻿	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatwaveAnimationBehavior : MonoBehaviour {

	float heat;
	Vector3 MinScale;
	Vector3 MaxScale;
	Animator anim;

	public float initialScale;
	public float endScale;

	// Use this for initialization
	void Start () {


		MinScale = new Vector3 (initialScale, initialScale, initialScale);
		MaxScale = new Vector3 (endScale, endScale, endScale);
		anim = GetComponent<Animator> ();
		transform.localScale = MinScale;
	}
	
	// Update is called once per frame
	void Update () {
		
			
		

		//as heat increases, heatwave gets bigger and faster

		
	}

    public void AnimateHeatWave(float heat)
    {
        
        if (heat < 0.5)
        {
            transform.localScale = Vector3.Lerp(MinScale, MaxScale, heat*2);
            anim.speed = 0.2f;
        }
        else
        {
            transform.localScale = MaxScale;
            //animate it moving faster
            anim.speed = (heat / 0.5f) * (heat / 0.5f) * (heat / 0.5f) * 0.2f;
        }
    }
}
