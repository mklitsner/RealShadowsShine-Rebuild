using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatwaveUI : MonoBehaviour
{
    [SerializeField] RectTransform HeatwaveGroup;
    [SerializeField] Transform target;
    [SerializeField] bool debug;
    [SerializeField] Vector3 offset;
    [SerializeField] float scalar;
    public bool isSecondary;

    private void Start()
    {
        if (!isSecondary)
        {
            HeatwaveGroup.parent = Manager.MainCanvas.transform;
            HeatwaveGroup.SetAsFirstSibling();

            HeatwaveGroup.localPosition = new Vector3(0, 0, 0.5f) ;
            HeatwaveGroup.localRotation = Quaternion.identity;
        }

        Destroy(transform.GetChild(0).gameObject);

    }

    private void Update()
    {
        if (!isSecondary)
        {
            Vector3 targetScreenPos = Manager.MainCanvas.worldCamera.WorldToScreenPoint(target.transform.position);

            if (debug)
            {
                Debug.Log("TargetScreenPos = " + targetScreenPos);
            }

            
            HeatwaveGroup.anchoredPosition = targetScreenPos + offset;
            HeatwaveGroup.localScale = new Vector3(scalar, scalar, scalar);
        }
        //rt.anchorMax = targetScreenPos;
        //rt.anchorMin = targetScreenPos;
    }
    

}
