using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public static Manager instance;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private SceneChange _sceneChange;

    public static AudioManager AudioManager;
    public static Canvas MainCanvas;
    public static SceneChange SceneChange;


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
        MainCanvas = instance._mainCanvas;
        SceneChange = instance._sceneChange;
    }
}
