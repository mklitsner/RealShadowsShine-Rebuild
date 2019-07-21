using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeSearcher : MonoBehaviour {
	public bool inshade;
	float Yposition;
	float Zposition;
	public bool testing=true;

	void Start () {
		Zposition = transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
		ScanForShade();
		bool inshade= GetComponent<DetectShade> ().inshade;

	}




	void ScanForShade(){
		float maxdistance =30;

		Yposition= transform.localPosition.y ;



		if (Yposition < maxdistance) {
			transform.Translate(0,0,1);
		} else if(Yposition>=maxdistance){
			transform.localPosition = new Vector3 (0, 2, Zposition);
		}

			
	}


}
