using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip[] flowerAudioClips;
    public List<AudioSource> audioSources= new List<AudioSource>();


    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            AddAudioSource();
        }
    }


    private AudioSource AddAudioSource()
    {
        GameObject go = new GameObject("AudioSource", typeof(AudioSource));
        AudioSource audioSource = go.GetComponent<AudioSource>();
        audioSource.transform.parent = transform;
        audioSources.Add(audioSource);
        return audioSource;
    }


    private AudioSource NextFreeAudioSource()
    {
       int length= audioSources.Count;
       bool isAllPlaying= true;

        for (int i = 0; i < length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                isAllPlaying = false;
                return audioSources[i];
            }
        }

        if (isAllPlaying)
        {
            return AddAudioSource();
        }
         
         return null;

    }

    public int GetRandomBloomSound()
    {
        return Random.Range(0, flowerAudioClips.Length);
    }

    public void PlayBloomSound(int index)
    {
        AudioSource audioSource =
        NextFreeAudioSource();
        audioSource.PlayOneShot(flowerAudioClips[index]);
    }

    public void PlayBloomSound(AudioClip customClip)
    {
        AudioSource audioSource =
        NextFreeAudioSource();
        audioSource.PlayOneShot(customClip);
    }


}
