using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Worm_Audio : MonoBehaviour {


	[SerializeField] AudioSource audiosource;
	// Use this for initialization
	bool on;

	public void PlayAudio(bool play)
    {
		if (play)
		{
			if (!audiosource.isPlaying)
			{
				audiosource.mute = false;
				audiosource.UnPause();
				on = true;
			}
			
		}
		else
		{
			audiosource.mute = true;
			audiosource.Pause();
			on = false;

		}

	}


    private void OnDrawGizmos()
    {
        if (audiosource.isPlaying)
        {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, 1f);
        }

    }
}
