using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfollowshadows : MonoBehaviour {
	//path follow

	public EditorPathScript PathToFollow;
    [SerializeField] Animator animator;
    [SerializeField] Worm_Audio audio;
    [SerializeField] float animationSpeed;
	public bool StartAtCurrentWaypoint;
	public int CurrentWayPointID = 0;
	private float reachDistance = 1.0f;
	public string pathName;

	bool inShadeAtLastCheck = false;

	public float currentspeed;

	public float rotationSpeed=1;

	public int FinalWaypoint;

    Activate_with_Tree activate_with_Tree;
    DetectShade detectShade;
    Vector3 last_position;
	Vector3 current_position;

	float timeToStartMovingAgain = float.NegativeInfinity;

	public enum ShadeWaitState {
		Moving,
		WaitingToLoseShade,
        LostShade,
		Dormant
	}

	public ShadeWaitState state = ShadeWaitState.Moving;

	public bool shouldMove;
    private float wait = 1;

    void Start () {
        

        if (StartAtCurrentWaypoint) {
			transform.position = PathToFollow.path_objs [CurrentWayPointID].position;
			transform.rotation=Quaternion.LookRotation (PathToFollow.path_objs [CurrentWayPointID+1].position - transform.position);
		}
        
        animator.speed = animationSpeed;

		if (GetComponent<Activate_with_Tree> ().activate==false) {
			state = ShadeWaitState.Dormant;
            StopAnimation = true;
            audio.PlayAudio(false);
        }
        
        activate_with_Tree = GetComponent<Activate_with_Tree>();
        detectShade = GetComponentInChildren<DetectShade>();
    }
	
	// Update is called once per frame
	void Update () {
        
        bool inshade = detectShade.inshade;

		float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);

        switch (state)
        {
            case ShadeWaitState.Moving:
                StopAnimation = false;
                transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * currentspeed);
                var rotation = Quaternion.LookRotation(PathToFollow.path_objs[CurrentWayPointID].position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

                CheckForNextTargetOnPath(distance);
                audio.PlayAudio(CurrentWayPointID != 0);


                if (inshade)
                {
                    StopAnimation = true;
                    audio.PlayAudio(false);
                    state = ShadeWaitState.WaitingToLoseShade;
                }
                break;
            case ShadeWaitState.WaitingToLoseShade:
                if (!inshade)
                {
                    timeToStartMovingAgain = Time.time + wait;
                    state = ShadeWaitState.LostShade;
                }

                break;
            case ShadeWaitState.Dormant:

                if (activate_with_Tree.activate)
                {
                    state = ShadeWaitState.Moving;
                }
                break;
            case ShadeWaitState.LostShade:
                if (Time.time > timeToStartMovingAgain)
                {
                    if (inshade)
                    {
                        state = ShadeWaitState.WaitingToLoseShade;
                    }
                    else
                    {
                        state = ShadeWaitState.Moving;
                    }
                   
                }
                break;
            default:
                break;


        }
    }


    private void CheckForNextTargetOnPath(float distance)
    {
        if (distance <= reachDistance)
        {
            CurrentWayPointID++;

        }

        if (CurrentWayPointID >= PathToFollow.path_objs.Count)
        {
            if (FinalWaypoint == -1)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                CurrentWayPointID = FinalWaypoint;
            }
        }
    }

    bool StopAnimation
    {
        get
        {
            return animator.GetBool("stop");
        }
        set
        {
            animator.SetBool("stop",value);
        }
    }




}
