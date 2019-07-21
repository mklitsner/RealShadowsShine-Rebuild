using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Worm_Audio : MonoBehaviour {


	AudioSource audiosource;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		


		if (GetComponent<Pathfollowshadows>().shouldMove && GetComponent<Pathfollowshadows>().CurrentWayPointID!=0 ) {
			if (!audiosource.isPlaying) {
				audiosource.UnPause();
			}
		} else {
			audiosource.Pause();
		}
	}
}
