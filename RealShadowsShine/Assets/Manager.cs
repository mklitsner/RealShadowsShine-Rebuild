using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public static Manager instance;

    [SerializeField] private AudioManager _audioManager;

    public static AudioManager AudioManager;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
       
        Initialize();
    }

    private void Initialize()
    {
        AudioManager = instance._audioManager;
    }
}
