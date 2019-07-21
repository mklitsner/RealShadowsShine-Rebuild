using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlowerAudio : MonoBehaviour {
	AudioSource audiosource;
	public AudioClip[] audioClips;
	bool clipPlayed;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		audiosource.clip = audioClips[Random.Range(0, audioClips.Length)];
		clipPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool playSound = GetComponent<BlendFlower> ().playSound;


		if (playSound && !clipPlayed) {
			audiosource.Play ();
			//Debug.Log (this.gameObject.name + "played sound");
			clipPlayed = true;
		}
	}
}
