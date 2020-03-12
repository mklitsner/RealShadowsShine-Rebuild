using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour {
	public bool initiate;
	[SerializeField] Text[] letters;
	[SerializeField] Animator animator;
	[SerializeField] DetectShade detectShade;
    private Color[] lettersColor;
    public float timeLeftinCycle;
	[SerializeField]BlendFlower blendFlower;
    

    // Use this for initialization
    void Start () {

		lettersColor = new Color[letters.Length];
        for (int i = 0; i < letters.Length; i++)
        {
			lettersColor[i]=letters[i].color;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
		if (detectShade.inshade)
        {
           ActivateAnimation(true);

            if (blendFlower.budState.Equals(BlendFlower.BudState.Bloomed))
            {
                initiate = true;
            }
        }
        else {
			
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
