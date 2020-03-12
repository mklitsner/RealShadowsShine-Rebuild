 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertWandererAI: MonoBehaviour { 
	[Range(0, 1)]public float heat;//0-1
	[Range(0, 1)]public float tiredness;//0-1
    [Range(0, 1)] public float shadedColorTimer;//0-1
    float shadedColorSpeed=0.1f;

    public GameObject footprint;
	public GameObject self;
	int footprintSide=1;

	const string resting = "resting";
	const string wandering ="wandering";
	public string state = wandering;

	float speed=1;
	public float currentspeed;

	float rotationSpeed;
    private GameObject footprintHolder;
    public float currentrotationSpeed;

    [SerializeField] BlendShapeBehavior blendShapeBehavior;
    [SerializeField] DetectShade detectShade;
    [SerializeField] Wanderer_Audio audioControl;
    [SerializeField] HeatwaveAnimationBehavior heatwave;
    [SerializeField] float footprintheight= 0.5f;

    //path follow


    //

    private void Awake()
    {
        footprintHolder = new GameObject("Footprints");
    }

    void Start () {
		currentspeed = 1;
		StartCoroutine (FootPrintTiming (5));
		state = resting;
		rotationSpeed=1;
        
        SetState (wandering);
		footprintSide = 1;
		//StartCoroutine (FootPrintTiming (1));


		speed = 2;
	


		//path follow


	}







	// Update is called once per frame
	void Update () {

		bool inshade = detectShade.inshade;

		//WHAT HAPPENS IF NOT IN THE SHADE

		if (state == wandering) {

			//path follow



			if (!inshade) {

                if (shadedColorTimer < 1)
                {
                    shadedColorTimer += shadedColorSpeed;
                }

                if (heat > 0.2f) {
					//if heated
					SetSpeed (0.2f / heat);
					MoveForward (currentspeed);
				} else {
					SetSpeed (1);
					MoveForward (currentspeed);
				}
			}


		//WHAT HAPPENS IF IN THE SHADE
		else if (inshade) {
				SetSpeed (1);
				MoveForward (currentspeed);

                if (shadedColorTimer > 0)
                {
                    shadedColorTimer -= shadedColorSpeed;
                }

            }
			if (tiredness >= 1) {
				SetState (resting);
			}
		}

		if (state == resting) {
			SetSpeed (0);
			if (heat <= 0 && tiredness <= 0) {
				SetState (wandering);
			}
		}


       

        SetHeat (0.005f,0.01f,inshade);
		SetTiredness (0.01f, 0.02f,inshade);

        audioControl.UpdateAudio(inshade, heat);
        blendShapeBehavior.UpdateBlendShape(inshade, heat, audioControl.shadedControl,shadedColorTimer);
        heatwave.AnimateHeatWave(heat);
    }

	void SetSpeed(float _multiplier){
		currentspeed = speed * _multiplier;
		currentrotationSpeed = rotationSpeed * _multiplier;

	}



	void SetState(string _state){

		switch (_state) {
		case resting:
			//goes to sleep for a short amount of time
			state=resting;

			break;

		case wandering:
			//gets up and continues walking
			state = wandering;
			break;
		}
	}




	void MoveForward(float _speed){
		//set wavering parameters
		//float newrotationFrequency = (heat + 1) * rotationFrequency;
		float _rotationFrequency =2;
		float _angle = Mathf.Cos (Time.time*_rotationFrequency) * currentrotationSpeed;

		transform.Translate (0, 0, _speed * Time.deltaTime);
		//waver
		transform.Rotate (0, _angle, 0);
	}
		




	int RandomSign(){
		if (Random.Range (0, 2) == 0) {
			return -1;
		} 
		return 1;
		
	}








	private IEnumerator FootPrintTiming(float _duration){

		while (true) {
			
			footprintSide = -1 * footprintSide;
			Vector3 footprintposition = new Vector3 (transform.localPosition.x + 0.05f * footprintSide, transform.localPosition.y - footprintheight, transform.localPosition.z);
			GameObject footprintclone = Instantiate (footprint, footprintposition, Quaternion.Euler (180 + footprintSide * 90, transform.localEulerAngles.y, 90 + 90 * footprintSide),footprintHolder.transform);
			footprintclone.GetComponent<FootprintScript> ().footprintSide = footprintSide;

			float timing = _duration / (currentspeed+4);
//			Debug.Log (timing);
			yield return new WaitForSeconds (timing);

		}
	}




	void SetHeat(float _increaseheat,float _decreaseheat,bool _inshade){
		if(_inshade){
			if(heat<=0){
				heat = 0;
			}else{
				heat = heat-_decreaseheat;
				}
			}
		if(!_inshade){
			if(heat>=1){
				heat = 1;
			}else{
				heat = heat +_increaseheat;
			}
		}
	}
		

	void SetTiredness(float _increasetiredness,float _decreasetiredness,bool _inshade){
		if (state != resting && !_inshade) {
			if (tiredness >= 1) {
				tiredness = 1;

			} else {
				tiredness = tiredness + _increasetiredness * heat;

			}
		}
		if (state == resting) {
			if (_inshade) {
				//if he falls asleep in the shade, he wakes up and goes back to wandering
				if (tiredness <= 0) {
				} else {
					tiredness = tiredness - _decreasetiredness;
				}
			}
		}
	}






	}


	


