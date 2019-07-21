using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientPathToGround : MonoBehaviour {

	// Use this for initialization
	public int orientaton;
	public float offset;
	public int TailUnaffected;
	void Start () {
		List<Transform> path_objs = GetComponent<EditorPathScript> ().path_objs;

		for(int i = 0; i < path_objs.Count-TailUnaffected; i++){
			Vector3 position = path_objs [i].position;
			if(i>=0){
				Ray ray = new Ray (path_objs [i].transform.position, orientaton*transform.up);
				Debug.Log ("ray" + ray);
				RaycastHit hit;
				if (Physics.SphereCast (ray, 0.1f, out hit)) 
				{
					Debug.Log ("hit at" + hit.point);
					path_objs [i].transform.position = new Vector3 (position.x, hit.point.y+offset, position.z);
			}
		}
	}
	}
	

}
