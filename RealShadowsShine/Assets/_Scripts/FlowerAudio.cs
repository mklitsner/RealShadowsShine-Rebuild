using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlowerAudio : MonoBehaviour {
    int soundIndex;
	bool clipPlayed;
    AudioManager audioManager;
    [SerializeField] AudioClip customSound;
    //[SerializeField] BlendFlower _blendFlower;

    void Start () {
        audioManager = Manager.AudioManager;
        soundIndex = audioManager.GetRandomBloomSound();
	}
	


    public void PlayBloomSound()
    {
        if (!clipPlayed)
        {
            if (customSound != null)
            {
                audioManager.PlayBloomSound(customSound);
            }
            else
            {
                audioManager.PlayBloomSound(soundIndex);
            }
         clipPlayed = true;
        }
    }
}
