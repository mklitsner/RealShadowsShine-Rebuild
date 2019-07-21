using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshBlend : MonoBehaviour
{

    public SkinnedMeshRenderer skinnedMeshRenderer;
    public int bend;
    public int bendHalf;
    public int melt;
    public int[] wrinkle;


    public void setBlend(int index, float blend)
    {
        skinnedMeshRenderer.SetBlendShapeWeight(index, blend);
    }

}
