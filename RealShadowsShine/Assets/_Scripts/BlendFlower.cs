using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendFlower : MonoBehaviour {
	[SerializeField]SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] DetectShade _detectShade;
    [SerializeField] heatWaveObjectBehavior heatWaveObject;
    [SerializeField] Transform riseTransform;
    [SerializeField] FlowerAudio flowerAudio;

    public float heat;
    float heatSpeed = 0.01f;

    public BloomType bloomType;

    public enum BloomType
    {
        Shade,Growth
    }


    public int petalType;

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
    public List<int> newBlends;
    List<float> blendMax;
    private string debugString;
    private float blendTime;

    // Use this for initialization
    void Start () {
       heat = 0;
        skinnedMesh = _skinnedMeshRenderer.sharedMesh;
		blendShapeCount = skinnedMesh.blendShapeCount;
        if(riseTransform!=null)
        riseTransform.Translate (0, -rise * transform.localScale.x, 0);

        if (Random.Range(0, 3) == 3)
        {
            petalType = Random.Range(0, 3);
        }
        else
        {
            petalType = 0;
        }

       

        SetPetals();
		



	}
	
	// Update is called once per frame
	void Update () {

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

        Heat();



        
    }


    void Heat()
    {
        if (_detectShade.inshade
            ||budState.Equals(BudState.Bloomed)
            ||budState.Equals(BudState.Blooming))
        {
            if (heat > 0)
                heat -= heatSpeed;
        }
        else
        {
            if (heat < 1)
                heat += heatSpeed;
          
        }
        if(heatWaveObject!=null)
        heatWaveObject.AnimateHeatWave(heat);
    }

    public void StartBloom()
    {
        if (budState.Equals(BudState.Closed))
        {
            StartCoroutine(Bloom(time));

            StartCoroutine(WaitToPlaySound(Sound_delay));
            
            //            Debug.Log(debugString, this);
        }

    }

    private IEnumerator WaitToPlaySound(float sound_delay)
    {
        yield return new WaitForSeconds(sound_delay);
            flowerAudio.PlayBloomSound();
    }

    IEnumerator Bloom(float bTime)
	{
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / bTime)
        {
            if (riseTransform != null)
                riseTransform.Translate(0, rise * transform.localScale.x / bTime * Time.deltaTime, 0);

            BlendPetals(t);
            blendTime = t;

            yield return budState = BudState.Blooming;
        }
        yield return budState= BudState.Bloomed;


    }


    void SetPetals()
    {
        List<int> blends = new List<int>();
        blendMax = new List<float>();
        float min=0;
        float max=100;
        int blendCount = 0;

        switch (petalType)
        {
            case 0:
                blends = new List<int>() { 0, 1, 2, 3};
                blendCount = Random.Range(0, 3);
                min = 30;
                max = 100;
                break;
            case 1:
                blends = new List<int>() { 5, 6};
                blendCount = 0;
                min = 20;
                max = 60;
                break;
            case 2:
                blends = new List<int>() { 0, 1, 2, 3, 5, 6 };
                blendCount = Random.Range(0, 3);
                min = 30;
                max = 100;
                break;
            default:
                break;
        }

      

        int length = blends.Count+1; 
        newBlends = blends;
        //random remove
        for (int i = 0; i < length - blendCount; i++)
        {
            int Rblend = Random.Range(0, newBlends.Count);

            newBlends.Remove(Rblend);
        }

        if (newBlends.Count == 2)
        {
            min = 20;
            max = 50;
        }
        else if (newBlends.Count >= 3)
        {
            min = 5;
            max = 15;
        }



        foreach (var Nblend in newBlends)
        {
            blendMax.Add(Random.Range(min, max));
            _skinnedMeshRenderer.SetBlendShapeWeight(Nblend, 0);
        }
       

        debugString = string.Format("min {0} max {1} count {4}(removed {2} of {3})", min, max, length - blendCount, length, newBlends.Count);
    }

    private void BlendPetals( float t)
    {

        for (int i = 0; i < newBlends.Count; i++)
        {
            _skinnedMeshRenderer.SetBlendShapeWeight(newBlends[i], Mathf.Lerp(0, blendMax[i], t));
        }

        
    }


}
