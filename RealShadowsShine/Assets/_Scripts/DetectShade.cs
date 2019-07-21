using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectShade : MonoBehaviour {

	public bool inshade;
	Vector3 sunPosition;
	public float spherecastSize = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	void OnDrawGizmos(){
		Vector3 position = transform.position;
		Gizmos.DrawWireSphere (position, 0.3f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			sunPosition = GameObject.Find ("sunTarget").transform.position;
			Ray ray = new Ray(transform.position, (sunPosition-transform.position));
			RaycastHit hit;
			//print (sunPosition - transform.position);
			Debug.DrawRay (transform.position, (sunPosition - transform.position));

		if (Physics.SphereCast (ray, 0.1f, out hit)) 
			{
				if (hit.transform.gameObject.name == "sunTarget") {
					inshade = false;
				} else {
					inshade = true;
				}
			}
		}
		

}
