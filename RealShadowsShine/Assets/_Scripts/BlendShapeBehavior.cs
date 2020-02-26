using UnityEngine;

public class BlendShapeBehavior : MonoBehaviour {
    [SerializeField] SkinnedMeshBlend[] skinnedMeshBlend;
    [SerializeField] DarkenWithHeat[] colorControl;
	public float bendHalfBlend1;
    public float bendBlend2;
    public float meltBlend7;
	public float blendSpeed=1;
	public float wrinkleSpeed=10;
	public float wrinkleIntensity;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
public void UpdateBlendShape (bool inshade, float heat, float shadedControl, float shadeTimer)
    {



        float heatBlend=Mathf.Lerp (0,100, heat);
	

		bendHalfBlend1 = Mathf.Lerp (bendHalfBlend1, heatBlend, Time.deltaTime*blendSpeed);

        float[] wrinkleBlend= new float[4];
        float[] wrinkleBlendTemp = new float[2]; ;
		
        wrinkleBlendTemp[0] = wrinkleBlend[0];
        wrinkleBlendTemp[1] = wrinkleBlend[1];



		if (inshade) {
            wrinkleBlend[0] = Mathf.Lerp (0, wrinkleIntensity, Mathf.Cos (Time.time * wrinkleSpeed));
            wrinkleBlend[1] = Mathf.Lerp (0, wrinkleIntensity, -Mathf.Cos(Time.time * wrinkleSpeed*1.3f));
            wrinkleBlend[2] = Mathf.Lerp (0, wrinkleIntensity, Mathf.Cos (Time.time * wrinkleSpeed*0.7f));
            wrinkleBlend[3] = Mathf.Lerp (0, wrinkleIntensity, -Mathf.Cos(Time.time * wrinkleSpeed*1.3f));
            wrinkleBlendTemp[0] = wrinkleBlend[0];
            wrinkleBlendTemp[1] = wrinkleBlend[1];

        }
        else {
            wrinkleBlend[0] = Mathf.Lerp (wrinkleBlendTemp[0], 0, shadedControl);
            wrinkleBlend[1] = Mathf.Lerp (wrinkleBlendTemp[1], 0, shadedControl);

		}

        for (int i = 0; i < skinnedMeshBlend.Length; i++)
        {


            skinnedMeshBlend[i].setBlend(skinnedMeshBlend[i].bendHalf, bendHalfBlend1);
            skinnedMeshBlend[i].setBlend(skinnedMeshBlend[i].bend, bendBlend2);

            for (int z = 0; z < skinnedMeshBlend[i].wrinkle.Length; z++)
            {
                skinnedMeshBlend[i].setBlend(skinnedMeshBlend[i].wrinkle[z], wrinkleBlend[z]);
            }
        }

        for (int i = 0; i < colorControl.Length; i++)
        {
            colorControl[i].SetShadeColor(shadeTimer);
        }
    }
}



