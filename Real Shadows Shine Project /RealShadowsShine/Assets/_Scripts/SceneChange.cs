using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
	GameObject wanderer;
	public string NextSceneName;
	public bool transition =false;
	public bool startTransition = false;
	GameObject Fader;
	public bool StartGame;
	// Use this for initialization
	void Start () {
		wanderer= GameObject.Find ("wanderer");
		Fader= GameObject.Find ("Fader");
	}
	
	// Update is called once per frame
	void Update () {

		bool changeScene;

		if (StartGame) {
			changeScene = wanderer.transform.GetComponent<PlayGame> ().initiate;
		} else {
			changeScene = wanderer.transform.GetComponent<WandererPathFollow> ().EndOfPath;
		}

		if (transition == false) {
			if (changeScene) {
				SceneManager.LoadScene (NextSceneName, LoadSceneMode.Single);
			}
		} else {
			if (changeScene) {
				startTransition = true;
				if (Fader.GetComponent<Fade> ().fadeOver) {
					SceneManager.LoadScene (NextSceneName, LoadSceneMode.Single);
				}
			}

		}
	}
}
