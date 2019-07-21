using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeBehavior : MonoBehaviour {
	SkinnedMeshRenderer skinnedMeshRenderer;
	Mesh skinnedMesh;
	int blendShapeCount;
	public float blendOne = 0f;
	public float blendSpeed=1;
	public float wrinkleSpeed=10;
	public float wrinkleIntensity;


	// Use this for initialization
	void Start () {
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		blendShapeCount = skinnedMesh.blendShapeCount; 
	}
	
	// Update is called once per frame
	void Update () {
		bool inshade = transform.parent.GetComponent<DetectShade> ().inshade;
		float heat = transform.parent.GetComponent<DesertWandererAI> ().heat;
		float shadedControl= transform.parent.GetComponent<Wanderer_Audio> ().shadedControl;
		float blendheat=Mathf.Lerp (100, 0, heat);
	

		blendOne = Mathf.Lerp (blendOne, blendheat, Time.deltaTime*blendSpeed);
			
		float blendTwo=0;
		float blendThree=0;
		float blendFour=0;
		float blendFive=0;
		float blendTwoTemp = blendTwo;;
		float blendThreeTemp = blendThree;



		if (inshade) {
			blendTwo = Mathf.Lerp (0, wrinkleIntensity, Mathf.Cos (Time.time * wrinkleSpeed));
			blendThree = Mathf.Lerp (0, wrinkleIntensity, -Mathf.Cos(Time.time * wrinkleSpeed*1.3f));
			blendFour = Mathf.Lerp (0, wrinkleIntensity, Mathf.Cos (Time.time * wrinkleSpeed*0.7f));
			blendFive = Mathf.Lerp (0, wrinkleIntensity, -Mathf.Cos(Time.time * wrinkleSpeed*1.3f));
			blendTwoTemp = blendTwo;
			blendThreeTemp = blendThree;
		} else {
			blendTwo = Mathf.Lerp (blendTwoTemp, 0, shadedControl);
			blendThree = Mathf.Lerp (blendThreeTemp, 0, shadedControl);

		}

		skinnedMeshRenderer.SetBlendShapeWeight (0, blendOne);
		skinnedMeshRenderer.SetBlendShapeWeight (1, blendTwo);
		skinnedMeshRenderer.SetBlendShapeWeight (2, blendThree);
		skinnedMeshRenderer.SetBlendShapeWeight (3, blendFour);
		skinnedMeshRenderer.SetBlendShapeWeight (4, blendFive);

	}
}
