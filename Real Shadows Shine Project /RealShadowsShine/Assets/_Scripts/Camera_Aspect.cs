using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Aspect : MonoBehaviour {
	public float Width = 7f;
	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = Width/(2*Camera.main.aspect);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
