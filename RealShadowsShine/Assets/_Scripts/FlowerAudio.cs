using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlowerAudio : MonoBehaviour {
    int soundIndex;
	bool clipPlayed;
    AudioManager audioManager;
    [SerializeField] BlendFlower _blendFlower;

    void Start () {
        audioManager = Manager.AudioManager;
        soundIndex = audioManager.GetRandomFlowerSound();
		clipPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (_blendFlower.playSound && !clipPlayed) {
            audioManager.PlayFlowerSound(soundIndex);
            //Debug.Log (this.gameObject.name + "played sound");
            clipPlayed = true;
		}
	}
}
