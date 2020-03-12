using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
	WandererPathFollow wanderer;
	[SerializeField] PlayGame playGame;
	public string NextSceneName;
	public bool transition =false;
	public bool startTransition = false;
	public Fade Fader;
	public bool StartGame;
	bool isGoBackToMenu;
	// Use this for initialization
	void Start () {
        if((WandererPathFollow)FindObjectOfType(typeof(WandererPathFollow))!=null)
		wanderer = (WandererPathFollow)FindObjectOfType(typeof(WandererPathFollow));

        Fader = (Fade)FindObjectOfType(typeof(Fade));
		isGoBackToMenu = false;
	}
	
	// Update is called once per frame
	void Update () {

		bool changeScene;


		if (StartGame) {
			changeScene = playGame.initiate;
		} else {
            if (isGoBackToMenu)
            {
				changeScene = true;

			}
            else
            {
				changeScene = wanderer.EndOfPath;
			}
		}

		if (transition == false) {
			if (changeScene)
            {
                ChangeScene();
            }
        } else {
			if (changeScene) {
				startTransition = true;
				if (Fader.fadeOver) {
					ChangeScene();
				}
			}

		}
	}

    private void ChangeScene()
    {
        if (isGoBackToMenu)
        {
            SceneManager.LoadScene("0_Menu", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(NextSceneName, LoadSceneMode.Single);
        }
    }

    public void GoBackToMenu()
	{
		if (!SceneManager.GetActiveScene().name.Equals("0_Menu"))
		{
			isGoBackToMenu = true;
		}
	}
}
