using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatWaveObjectBehavior : MonoBehaviour {

    public bool fixWorldSize;
    public float worldSize = 1;
    [SerializeField] Vector3 fixedRotation;
    [SerializeField] bool FixRotationOn;
    [SerializeField] HeatwaveAnimationBehavior[] heatwaveAnimations;
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (FixRotationOn)
        transform.rotation = Quaternion.Euler(fixedRotation.x, fixedRotation.y, fixedRotation.z);

        



        

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
