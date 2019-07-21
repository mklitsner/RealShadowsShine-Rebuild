﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfollowshadows : MonoBehaviour {
	//path follow

	public EditorPathScript PathToFollow;
	public bool StartAtCurrentWaypoint;
	public int CurrentWayPointID = 0;
	private float reachDistance = 1.0f;
	public string pathName;

	bool inShadeAtLastCheck = false;

	public float currentspeed;

	public float rotationSpeed=1;

	public int FinalWaypoint;

	Vector3 last_position;
	Vector3 current_position;
	//
	// Use this for initialization


	float timeToStartMovingAgain = float.NegativeInfinity;
	public enum ShadeWaitState {
		Moving,
		WaitingToLoseShade,
		Dormant
	}

	public ShadeWaitState state = ShadeWaitState.Moving;

	public bool shouldMove;

	void Start () {
		
		if (StartAtCurrentWaypoint) {
			transform.position = PathToFollow.path_objs [CurrentWayPointID].position;
			transform.rotation=Quaternion.LookRotation (PathToFollow.path_objs [CurrentWayPointID+1].position - transform.position);
		}

		if (GetComponent<Activate_with_Tree> ().activate==false) {
			state = ShadeWaitState.Dormant;
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool inshade = GetComponentInChildren<DetectShade> ().inshade;


		float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);



		if (state == ShadeWaitState.Dormant) {
			if (GetComponent<Activate_with_Tree> ().activate) {
				state = ShadeWaitState.Moving;
			}
		} else {
			
		
			shouldMove = 
			//!inshade; //original
			state == ShadeWaitState.Moving;

			if (shouldMove) {
				transform.position = Vector3.MoveTowards (transform.position, PathToFollow.path_objs [CurrentWayPointID].position, Time.deltaTime * currentspeed);
			}




			var rotation = Quaternion.LookRotation (PathToFollow.path_objs [CurrentWayPointID].position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);


			if (distance <= reachDistance) {
				CurrentWayPointID++;

			}

			if (CurrentWayPointID >= PathToFollow.path_objs.Count) {
				if (FinalWaypoint == -1) {
					Destroy (transform.parent.gameObject);
				} else {
					CurrentWayPointID = FinalWaypoint;
				}
			}




			if (inshade) { //&& !inShadeAtLastCheck) { //instant enter shade

				state = ShadeWaitState.WaitingToLoseShade;
				timeToStartMovingAgain = Time.time + 1;
		
			} 

			if (Time.time > timeToStartMovingAgain) {
				state = ShadeWaitState.Moving;
			}



			inShadeAtLastCheck = inshade;
		}
	}
}
