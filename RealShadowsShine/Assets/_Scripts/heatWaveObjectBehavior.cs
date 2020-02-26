using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatWaveObjectBehavior : MonoBehaviour {

    public bool fixWorldSize;
    public float worldSize = 1;
    [SerializeField] HeatwaveAnimationBehavior[] heatwaveAnimations;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.Euler (0, 0, 0);

        Transform parent = transform.parent;

        transform.localScale = new Vector3(worldSize / parent.transform.localScale.x, worldSize / parent.transform.localScale.y, worldSize / parent.transform.localScale.z);


    }

    public void AnimateHeatWave(float heat)
    {
        for (int i = 0; i < heatwaveAnimations.Length; i++)
        {
            heatwaveAnimations[i].AnimateHeatWave(heat);
        }
    }
}
