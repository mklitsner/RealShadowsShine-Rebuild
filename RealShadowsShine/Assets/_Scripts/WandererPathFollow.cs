using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererPathFollow : MonoBehaviour {
	public EditorPathScript PathToFollow;

	public int CurrentWayPointID;
	private float reachDistance = 1.0f;
	public string pathName;
	public bool EndOfPath;

	Vector3 current_position;
	// Use this for initialization
	void Start () {
		PathToFollow = GameObject.Find(pathName).GetComponent<EditorPathScript>();
	}
	
	// Update is called once per frame
	void Update () {
		float currentrotationSpeed = GetComponent<DesertWandererAI> ().currentrotationSpeed;

		float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
		//transform.position = Vector3.MoveTowards (transform.position, PathToFollow.path_objs [CurrentWayPointID].position, Time.deltaTime*currentspeed);

		var rotation = Quaternion.LookRotation (PathToFollow.path_objs [CurrentWayPointID].position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * currentrotationSpeed);


		if (distance <= reachDistance) {
			CurrentWayPointID++;

		}
		if (CurrentWayPointID >= PathToFollow.path_objs.Count) {
			EndOfPath = true;
		}
	}
}
