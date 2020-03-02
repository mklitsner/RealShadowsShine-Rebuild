using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOSC : MonoBehaviour {
	
	public Transform osc;
	[SerializeField] bool UseOSC;
	public string oscName;
	public float speed = 0.1F;
	float offset;

	void Start(){
		if (UseOSC)
		{
			osc = GameObject.Find(oscName).transform;
		}
	}
	void Update() {
		if (!UseOSC)
		{
			if (Input.GetKey("left") || Input.GetKey("right"))
			{
				offset = transform.eulerAngles.y;
			}
			else
			{
				float angle = Mathf.LerpAngle(transform.eulerAngles.y, offset, speed);
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
			}
		}
		else
		{
			float angle = Mathf.LerpAngle(transform.eulerAngles.y, osc.eulerAngles.y + offset, speed);
			//Debug.Log ("osc angle " + osc.eulerAngles.y);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
		}
	}
}
	