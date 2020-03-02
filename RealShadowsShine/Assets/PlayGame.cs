using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour {
	public bool initiate;
	[SerializeField] Text[] letters;
	[SerializeField] Animator animator;
	float counter;
	float limit;
    private Color[] lettersColor;
    public float timeLeftinCycle;

    // Use this for initialization
    void Start () {
		counter = 0;
		limit = 2;
		lettersColor = new Color[letters.Length];
        for (int i = 0; i < letters.Length; i++)
        {
			lettersColor[i]=letters[i].color;
        }
	}
	
	// Update is called once per frame
	void Update () {
		bool inshade = GetComponent<DetectShade> ().inshade;

		if (inshade)
        {
            //AnimateLetters(3,0f);

           ActivateAnimation(true);

            counter += Time.deltaTime;
            if (counter > 2)
            {
                initiate = true;
            }



        }
        else {
			counter = 0;
			ActivateAnimation(false);
		}
		
	}

    private void ActivateAnimation(bool activate)
    {
        animator.SetBool("active", activate);
    }

    private void AnimateLetters(float cycleTime, float overlap)
    {
      

		for (int i = 0; i < letters.Length; i++)
        {


			float letterTimeStart=
			cycleTime - (letters.Length-i-1) * cycleTime / (letters.Length);
			float letterTimeEnd =
				letterTimeStart - cycleTime / (letters.Length)-overlap;




			if (timeLeftinCycle < letterTimeStart)
            {
				//do animation
				Debug.Log(string.Format("{0} starts at {1} until {2}: {3}",
				letters.Length - i, letterTimeStart, letterTimeEnd,timeLeftinCycle));

                

				float duration = letterTimeStart - letterTimeEnd;
				float lerp;

				if (timeLeftinCycle>letterTimeEnd + duration / 2)
                {
					lerp=(timeLeftinCycle - letterTimeEnd+ duration / 2)/duration;
                    //approaching the peak of the color
					letters[i].color =
                        Color.Lerp(Color.white,lettersColor[i],
						lerp);
				
                }
                else
                {
					lerp = (timeLeftinCycle - letterTimeEnd)/duration;
					letters[i].color =
                        Color.Lerp(lettersColor[i], Color.white,
						lerp);

				}
				Debug.Log("color lerp is " + lerp);


			}

		}
		if (timeLeftinCycle <= 0)
		{
			timeLeftinCycle = cycleTime;
		}

		timeLeftinCycle -= Time.deltaTime;


	}
}
