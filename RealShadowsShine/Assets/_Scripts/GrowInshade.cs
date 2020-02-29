using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowInshade : MonoBehaviour {


    [SerializeField] DetectShade _detectShade;
    [SerializeField] BlendFlower _blendFlower;

    public Vector3 minScale;
	public Vector3 maxScale;

	public float growSpeed = 2f;
	public float shrinkSpeed = 2f;

	public float duration = 5f;
	public float stayLimit=1.1f;
	public bool stay;
	//set to above one if you want a stay 
	public float scaleIndex = 0.0f;
	public bool maxScaleIsStart;
	public bool growSound;

	void Start(){
		if(maxScaleIsStart){
			maxScale = transform.localScale;
		}
		transform.localScale = minScale;
	}
	// Use this for initialization


	void Update(){

		growSound = false;
		float rate_1 = (1.0f / duration) * growSpeed;
		float rate_2 = (1.0f / duration) * shrinkSpeed;

		if (_detectShade.inshade) {
			if (scaleIndex < 1.0f) {
				scaleIndex += Time.deltaTime * rate_1;
				growSound = true;
			}
		} else {
			
			if (scaleIndex > 0.0f&& !stay) {
				scaleIndex -= Time.deltaTime * rate_2;
			}
		}
		if (scaleIndex > stayLimit) {
			stay = true;
        }

       if(scaleIndex > 1.0f)
        {
            _blendFlower.StartBloom();
        }

        transform.localScale = Vector3.Lerp (minScale, maxScale, scaleIndex);
	}


		//if in shade long enough, stays grown
		

}
