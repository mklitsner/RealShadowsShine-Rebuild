using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendFlower : MonoBehaviour {
	[SerializeField]SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] DetectShade _detectShade;
    [SerializeField] heatWaveObjectBehavior heatWaveObject;
    [SerializeField] Transform riseTransform;
    [SerializeField] FlowerAudio flowerAudio;
    [SerializeField] MeshCollider[] meshCollider;
    [SerializeField] Color[] petalColors;

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
    



	[HideInInspector]
	public bool playSound;
	public bool bloomed;
	public float riseHeight;
    public float startHeight;
	public float time;

	public float Sound_delay=50;
    public List<int> newBlends;
    private bool triggerDebug;
    List<float> blendMax;
    private float blendTime;
    private string debugString;


    // Use this for initialization
    void Start () {

        if(petalColors.Length>0)
        _skinnedMeshRenderer.material.color =
            petalColors[Random.Range(0, petalColors.Length)];

        foreach (MeshCollider col in meshCollider)
        {
            col.enabled = false;
        }
       heat = 0;
        skinnedMesh = _skinnedMeshRenderer.sharedMesh;
		blendShapeCount = skinnedMesh.blendShapeCount;
        if(riseTransform!=null)
        riseTransform.Translate (0, startHeight * transform.localScale.x, 0);

        if (Random.Range(0, 3) == 2)
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
        if(heatWaveObject!=null&& heatWaveObject.gameObject.activeInHierarchy)
        heatWaveObject.AnimateHeatWave(heat);
    }

    public void StartBloom()
    {
        if (budState.Equals(BudState.Closed))
        {
            StartCoroutine(Bloom(time));

            StartCoroutine(WaitToPlaySound(Sound_delay));

            if(triggerDebug)
                     Debug.Log(debugString, this);
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
                riseTransform.Translate(0, riseHeight * transform.localScale.x / bTime * Time.deltaTime, 0);

            BlendPetals(t);
            blendTime = t;

            yield return budState = BudState.Blooming;
        }
        foreach (MeshCollider col in meshCollider)
        {
            col.enabled = true;
        }
        
        yield return budState= BudState.Bloomed;


    }


    void SetPetals()
    {
        List<int> blends = new List<int>(0);
        blendMax = new List<float>();
        float min=20;
        float max=100;
        int blendSubtract = 1;

        switch (petalType)
        {
            case 0:
                blends = new List<int>() { 0, 1, 2, 3};
                blendSubtract = Random.Range(1, 3);
                min = 30;
                max = 100;
                break;
            case 1:
                blends = new List<int>() { 5, 6};
                blendSubtract = 1;
                min = 20;
                max = 80;
                break;
            case 2:
                blends = new List<int>() { 0, 1, 2, 3, 5, 6 };
                blendSubtract = Random.Range(1, 3);
                min = 30;
                max = 100;
                break;
            default:
                break;
        }

      

        
        newBlends = blends;
        //random remove
        

            for (int i = 0; i < blends.Count-blendSubtract; i++)
            {
                int Rblend = Random.Range(0, newBlends.Count);

                newBlends.Remove(Rblend);
            }
        

        if (newBlends.Count == 2)
        {
            min = 20;
            max = 70;
        }
        else if (newBlends.Count >= 3)
        {
            min = 5;
            max = 30;
        }

        if (newBlends.Count < 1)
        {
            Debug.Log("AHHH",this);
            newBlends = new List<int>(Random.Range(0, 3));
            triggerDebug = true;
        }

        foreach (var Nblend in newBlends)
        {
            blendMax.Add(Random.Range(min, max));
            _skinnedMeshRenderer.SetBlendShapeWeight(Nblend, 0);
        }


        debugString = string.Format("min {0} max {1} count {2})", min, max, newBlends.Count);
    }

    private void BlendPetals( float t)
    {
        
        for (int i = 0; i < newBlends.Count; i++)
        {
            _skinnedMeshRenderer.SetBlendShapeWeight(newBlends[i], Mathf.Lerp(0, blendMax[i], t));
        }

        
    }


}
