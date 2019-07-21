using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOSC : MonoBehaviour {
	
	public Transform osc;
	public string oscName;
	public float speed = 0.1F;
	float offset;

	void Start(){
		osc=GameObject.Find (oscName).transform;
	}
	void Update() {
		
		if (Input.GetKey ("left") || Input.GetKey ("right")) {
			offset = transform.eulerAngles.y;
		} else {
			float angle = Mathf.LerpAngle (transform.eulerAngles.y, osc.eulerAngles.y+offset, speed);
			Debug.Log ("osc angle " + angle);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, angle, transform.eulerAngles.z);
		}
	}
}
	