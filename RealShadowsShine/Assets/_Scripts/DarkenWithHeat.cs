﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenWithHeat : MonoBehaviour {

	public Color currentColor;

	public Color startColor;
	public Color ScorchedColor;
	// Use this for initialization

	
	// Update is called once per frame
	public void SetShadeColor (float shadedColorTimer) {
		transform.GetComponent<SkinnedMeshRenderer> ().material.color=currentColor;
		

		currentColor = Color.Lerp (startColor, ScorchedColor, shadedColorTimer);
		
	}
  
}
