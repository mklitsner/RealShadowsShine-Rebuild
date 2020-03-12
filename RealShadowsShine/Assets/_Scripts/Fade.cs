using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
	float alphaFadeValue=1;
	public GameObject gameManager;
	public bool fadedIn=false;
	public bool fadeOver=false;
	public float fadeDuration=5;
	Color imageColor;
	// Use this for initialization
	void Start () {
		gameManager = Manager.instance.gameObject;
		alphaFadeValue=1;
		imageColor = GetComponent<Image>().color;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadedIn) {
			if (gameManager.GetComponent<SceneChange> ().startTransition) {
				alphaFadeValue += Mathf.Clamp01 (Time.deltaTime / fadeDuration);


				GetComponent<Image>().color = new Color (imageColor.r, imageColor.g, imageColor.b, alphaFadeValue);

				if (alphaFadeValue >= 1) {
					fadeOver = true;
				}
			}
		} else {
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 5);


			GetComponent<Image>().color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaFadeValue);
			if (alphaFadeValue <= 0) {
				fadedIn = true;
			}
		}

	
	}
}
