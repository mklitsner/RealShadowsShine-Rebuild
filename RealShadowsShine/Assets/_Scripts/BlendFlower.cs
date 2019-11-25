using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendFlower : MonoBehaviour {
	[SerializeField]SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] DetectShade _detectShade;
    [SerializeField]

    public BloomType bloomType;

    public enum BloomType
    {
        Shade,Growth
    }

    public BudState budState= BudState.Closed;

    public enum BudState
    {
        Closed,Blooming,Bloomed
    }

    Mesh skinnedMesh;
	int blendShapeCount;
	public int blendShapeSelected;

	bool blooming;
	[HideInInspector]
	public bool playSound;
	public bool bloomed;
	public float rise;
	public float time;
	float blend;
	public float Sound_delay=50;

	// Use this for initialization
	void Start () {

		skinnedMesh = _skinnedMeshRenderer.sharedMesh;
		blendShapeCount = skinnedMesh.blendShapeCount;

        transform.Translate (0, -rise, 0);
		
		_skinnedMeshRenderer.SetBlendShapeWeight (blendShapeSelected, 0);



	}
	
	// Update is called once per frame
	void Update () {

        /*
		if (transform.GetComponent<BlendFlowerChild> () != null) {

            Debug.LogError("BlendFlowerChild");
			if (!blooming) {
				if (transform.parent.GetComponent<GrowInshade> () != null) {
					if (transform.parent.GetComponent<GrowInshade> ().scaleIndex>0.9f) {
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
			*/           

            switch (bloomType)
            {
                case BloomType.Shade:
                    if (_detectShade.inshade)
                    {
                        StartBloom();
                    }
                    break;
                case BloomType.Growth:
                    //wait for command from growth
                    break;
                default:
                    break;
            }
				
			

		if (_skinnedMeshRenderer.GetBlendShapeWeight(blendShapeSelected) > Sound_delay) {
			playSound = true;
			Debug.Log ("played sound");
		}
	}



    public void StartBloom()
    {
        if (budState.Equals(BudState.Closed))
        {
            StartCoroutine(Bloom(100, time));
        }

    }

    IEnumerator Bloom(float bValue,float bTime)
	{
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / bTime)
		{
			transform.Translate (0, rise / bTime*Time.deltaTime,0);
			_skinnedMeshRenderer.SetBlendShapeWeight (blendShapeSelected,Mathf.Lerp(blend,bValue,t));
			yield return budState=BudState.Blooming;
		}
		yield return budState= BudState.Bloomed;


    }
		
}
