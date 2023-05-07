using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    
    
    public int level;
    public float mass;
    [FormerlySerializedAs("DebugText")]

    public int sampleWindow;
    public double velocityX;
    public bool fromMicrophone;
    
    public double floatingMultiplier;
    public double forceYMultiplier;
    public double walkThreshold;

    public double sensitivity;
    public double defaultSensitivity;
    private void Awake()
    {
        MakeInstance();
        SetParametersDefault();
    }

    private void SetParametersDefault()
    {
        fromMicrophone = true;
        floatingMultiplier = 3;
        forceYMultiplier = 250;
        walkThreshold = 0.015;
        sensitivity = !fromMicrophone ? 0.8 : 1;
        #if UNITY_ANDROID && !UNITY_EDITOR
            fromMicrophone = true;
            sensitivity =1.5;
        #else
            level = 2;
            sensitivity =0.8;
        #endif

        defaultSensitivity = sensitivity;
        sampleWindow = 3;
        velocityX = 2;
        mass = (float) (floatingMultiplier * walkThreshold * forceYMultiplier / 10);
        

    }

    void MakeInstance(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetParameters(float sensitivity)
    {
        this.sensitivity = sensitivity;
    }

    public void SetNextLevel()
    {
        level += 1;
        if (level > 5)
            level = 5;
    }
}
