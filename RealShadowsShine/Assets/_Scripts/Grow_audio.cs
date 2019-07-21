using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Grow_audio : MonoBehaviour {
	AudioSource audiosource;
	//public AudioClip[] audioClips;
	// Use this for initialization
	float StartPitch;
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		//audiosource.clip = audioClips[Random.Range(0, audioClips.Length)];
		//audiosource.Play ();
		//audiosource.Pause ();
		audiosource.enabled = false;
		StartPitch = audiosource.pitch;
	}

	// Update is called once per frame
	void Update () {
		bool inshade = GetComponent<GrowInshade> ().inshade;
		float i = GetComponent<GrowInshade> ().i;
		if (inshade && i < 1) {
			audiosource.enabled = true;
			audiosource.UnPause ();

			//play audio
			//pitch is mapped to i

		} else {
			audiosource.Pause ();

		}
			
		if (inshade) {
			audiosource.enabled = true;
		}

		if (i >= 1) {
			audiosource.enabled = false;
		}
		audiosource.pitch= Mathf.Lerp (StartPitch, StartPitch+0.5f*StartPitch, i);
	}
}
