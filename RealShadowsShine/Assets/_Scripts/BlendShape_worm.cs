using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShape_worm : MonoBehaviour {
	SkinnedMeshRenderer skinnedMeshRenderer;
	Mesh skinnedMesh;
	int blendShapeCount;

	public float wrinkleSpeed=10;
	public float wrinkleIntensity;
	float blendTwo=0;
	float blendOne=0;
	// Use this for initialization
	void Start () {
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		blendShapeCount = skinnedMesh.blendShapeCount; 
	}
	
	// Update is called once per frame
	void Update () {
		bool shouldMove = GetComponentInParent<Pathfollowshadows> ().shouldMove ;


		if (shouldMove) {
			blendTwo = Mathf.Lerp (0, wrinkleIntensity, Mathf.Cos (Time.time * wrinkleSpeed));
			blendOne = Mathf.Lerp (0, wrinkleIntensity, -Mathf.Cos(Time.time * wrinkleSpeed*1.3f));

			skinnedMeshRenderer.SetBlendShapeWeight (0, blendOne);
			skinnedMeshRenderer.SetBlendShapeWeight (1, blendTwo);

	}
}
}
