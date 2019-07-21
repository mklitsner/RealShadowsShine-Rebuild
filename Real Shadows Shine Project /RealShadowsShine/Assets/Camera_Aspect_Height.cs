using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Aspect_Height : MonoBehaviour {
	public float Height = 7f;
	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = (Camera.main.aspect)*Height;
	}
	void Update(){
		Camera.main.orthographicSize = (Camera.main.aspect)*Height;
	}
}
