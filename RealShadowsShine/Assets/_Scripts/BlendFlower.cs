using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendFlower : MonoBehaviour {
	SkinnedMeshRenderer skinnedMeshRenderer;
	Mesh skinnedMesh;
	int blendShapeCount;
	public int blendShapeSelected;
	[HideInInspector]
	public bool blooming;
	[HideInInspector]
	public bool playSound;
	public bool bloomed;
	public float rise;
	public float time;
	float blend;
	bool start;
	public float Sound_delay=50;

	// Use this for initialization
	void Start () {




	
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		blendShapeCount = skinnedMesh.blendShapeCount;

		if (rise != 0) {
			transform.Translate (0, -rise, 0);
		}
		skinnedMeshRenderer.SetBlendShapeWeight (blendShapeSelected, 0);



	}
	
	// Update is called once per frame
	void Update () {


		if (transform.GetComponent<BlendFlowerChild> () != null) {
			if (!blooming) {
				if (transform.parent.GetComponent<GrowInshade> () != null) {
					if (transform.parent.GetComponent<GrowInshade> ().i>0.9f) {
						StartCoroutine (Bloom (100, time));
					}
				} 
				if (transform.parent.GetComponent<BlendFlower> () != null) {
					if (transform.parent.GetComponent<BlendFlower> ().bloomed) {
						StartCoroutine (Bloom (100, time));
					}
				}
			}
			
			} else {
				bool inshade = GetComponentInChildren<DetectShade> ().inshade;
			if (inshade & !blooming) {
					//start blooming until fully bloomed
				//play audio clip

					StartCoroutine (Bloom (100, time));	
				}

				
			}

		if (skinnedMeshRenderer.GetBlendShapeWeight(blendShapeSelected) > Sound_delay) {
			playSound = true;
			Debug.Log ("played sound");
		}


	}



	IEnumerator Bloom(float bValue,float bTime)
	{
		
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / bTime)
		{
			
			transform.Translate (0, rise / bTime*Time.deltaTime,0);
			skinnedMeshRenderer.SetBlendShapeWeight (blendShapeSelected,Mathf.Lerp(blend,bValue,t));
			yield return blooming=true;

		
		}
		yield return bloomed=true;


	}
		
}
