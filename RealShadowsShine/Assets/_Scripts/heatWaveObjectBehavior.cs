using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatWaveObjectBehavior : MonoBehaviour {

    public bool fixWorldSize;
    public float worldSize = 1;
    [SerializeField] Vector3 fixedRotation;
    [SerializeField] bool FixRotationOn;
    [SerializeField] HeatwaveAnimationBehavior[] heatwaveAnimations;
    [SerializeField] bool isLookAtCamera;
    [SerializeField] Transform rotator;
   


	
	// Update is called once per frame
	void Update () {

        Transform parent = transform.parent;

        transform.localScale = new Vector3(worldSize / parent.transform.localScale.x,
            worldSize / parent.transform.localScale.y, worldSize / parent.transform.localScale.z);

        if (FixRotationOn)
        {
            Transform rotParent = rotator.parent;
            rotator.parent = null;
            rotator.rotation = Quaternion.Euler(fixedRotation.x, fixedRotation.y, fixedRotation.z);
            rotator.parent = rotParent;
        }

        if (isLookAtCamera)
        {
            Transform rotParent = rotator.parent;
            rotator.parent = null;

            Quaternion rotation =
            rotator.rotation;
            rotator.LookAt(Camera.main.transform,Vector3.forward);

            Quaternion lookRotation= rotator.rotation;
            rotator.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
            rotator.parent = rotParent;
        }
    }

    public void AnimateHeatWave(float heat)
    {
        for (int i = 0; i < heatwaveAnimations.Length; i++)
        {
            heatwaveAnimations[i].AnimateHeatWave(heat);
        }
    }
}
