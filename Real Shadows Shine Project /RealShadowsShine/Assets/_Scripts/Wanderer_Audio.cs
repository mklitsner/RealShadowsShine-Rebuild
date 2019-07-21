using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Wanderer_Audio : MonoBehaviour {
	

	public AudioMixer mastermix;
	[HideInInspector]
	public float shadedControl;
	public float controlSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float heatVol;


		float inshadeVol;
		float inshadePitch;


		bool inshade = GetComponent<DetectShade> ().inshade;
		float heat = GetComponent<DesertWandererAI>().heat;





		if (inshade) {
			if (shadedControl <= 1) {
				shadedControl += Time.deltaTime * controlSpeed;
			}



		} else {
			if (shadedControl >= 0) {
				shadedControl -= Time.deltaTime * controlSpeed;
			}
		}
		inshadeVol =  Mathf.Lerp (-30, 0, shadedControl);
		inshadePitch =  Mathf.Lerp (.1f, 1.00f, shadedControl);
			
		heatVol = Mathf.Lerp (-80, 0, heat);

		mastermix.SetFloat ("shadevol", inshadeVol);
		mastermix.SetFloat ("shadepitch", inshadePitch);
		mastermix.SetFloat ("hotvol", heatVol);
			
	}


}
