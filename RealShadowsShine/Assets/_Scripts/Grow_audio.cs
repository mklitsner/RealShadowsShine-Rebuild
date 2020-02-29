using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Grow_audio : MonoBehaviour {
	AudioSource audiosource;
	float StartPitch;
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		//audiosource.clip = audioClips[Random.Range(0, audioClips.Length)];
		audiosource.mute = true;
		audiosource.Play ();
		
		StartPitch = audiosource.pitch;
	}

	// Update is called once per frame
	void Update () {
		bool growSound = GetComponent<GrowInshade> ().growSound;
		float height = GetComponent<GrowInshade> ().scaleIndex;
		if (growSound) {
			audiosource.enabled = true;

			audiosource.mute = false;

		} else {
			audiosource.mute = true;
			audiosource.enabled = false;
		}

		audiosource.pitch= Mathf.Lerp (StartPitch, StartPitch+0.5f*StartPitch, height);
	}
}
