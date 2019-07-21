﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowInshade : MonoBehaviour {


	public Vector3 minScale;
	public Vector3 maxScale;

	public float growSpeed = 2f;
	public float shrinkSpeed = 2f;

	public float duration = 5f;
	public float stayLimit=1.1f;
	public bool stay;
	//set to above one if you want a stay 
	public float i = 0.0f;
	public bool maxScaleIsStart;
	public bool inshade;

	void Start(){
		if(maxScaleIsStart){
			maxScale = transform.localScale;
		}

		transform.localScale = minScale;
	}
	// Use this for initialization


	void Update(){
		inshade = transform.parent.GetComponentInChildren<DetectShade> ().inshade;

		float rate_1 = (1.0f / duration) * growSpeed;
		float rate_2 = (1.0f / duration) * shrinkSpeed;

		if (inshade) {
			if (i < 1.0f) {
				i += Time.deltaTime * rate_1;
			}
		} else {
			if (i > 0.0f&& !stay) {
				i -= Time.deltaTime * rate_2;
			}

		}
		if (i > stayLimit) {
			stay = true;
		}
		transform.localScale = Vector3.Lerp (minScale, maxScale, i);
	}


		//if in shade long enough, stays grown
		

}
