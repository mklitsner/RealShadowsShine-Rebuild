using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlowerAudio : MonoBehaviour {
    int soundIndex;
	bool clipPlayed;
    AudioManager audioManager;
    [SerializeField] AudioClip[] customSounds;
    AudioClip customSound;

    void Start () {
        audioManager = Manager.AudioManager;
        soundIndex = audioManager.GetRandomBloomSound();

        if (customSounds.Length > 0)
        {
            customSound = customSounds[Random.Range(0, customSounds.Length)];
        }
    }
	


    public void PlayBloomSound()
    {
        if (!clipPlayed)
        {
            if (customSound!=null)
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
