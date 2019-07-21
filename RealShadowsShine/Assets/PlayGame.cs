using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour {
	public bool initiate;
	float counter;
	float limit;
	// Use this for initialization
	void Start () {
		counter = 0;
		limit = 2;
	}
	
	// Update is called once per frame
	void Update () {
		bool inshade = GetComponent<DetectShade> ().inshade;

		if (inshade) {
			counter += Time.deltaTime;
			if (counter > 2) {
				initiate = true;
			}
		} else {
			counter = 0;
		}
		
	}
}
