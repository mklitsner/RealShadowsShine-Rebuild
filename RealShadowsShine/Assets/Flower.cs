using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Flower : MonoBehaviour
{
    [SerializeField] bool FreezeScale;
    [SerializeField] bool Secondary;
    [SerializeField] GameObject heatIndicator;
    void Awake()
    {
        if (Application.isPlaying)
            Destroy(this);
    }
    bool runOnce;
    private bool m_OldFreezeScale;
    private Vector3 m_Scale;

    private void Update()
    {

        if (!Application.isEditor)
        {
            Destroy(this);
            return;
        }

        if (!runOnce)
        {
            Debug.Log("inEditorMode");
            runOnce = true;
        }

        if (FreezeScale)
        {
            // Save current position if enabled
            if (FreezeScale != m_OldFreezeScale)
                m_Scale = transform.localScale;
            // Freeze the position
            transform.localScale = m_Scale;
        }

        m_OldFreezeScale = FreezeScale;

        if (Secondary)
        {
            heatIndicator.SetActive(false);
        }
        else
        {
            heatIndicator.SetActive(true);
        }
    }

   

}
