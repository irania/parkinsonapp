using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AudioJumpHandler : MonoBehaviour
{
    public AudioSource source;
    public double loudness = 0;


    private void Start()
    {
        if(Settings.Instance.fromMicrophone)
            source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        loudness = Settings.Instance.fromMicrophone ? 
            AudioLoudnessDetector.Instance.GetLoudnessFromMicrophone() : 
            AudioLoudnessDetector.Instance.GetLoudnessFromAudioClip(source.timeSamples,source.clip);

        loudness *= Settings.Instance.sensitivity;
        
        if (loudness > Settings.Instance.walkThreshold)
        {
            Debug.Log(loudness);
            PlayerFlyScript.Instance.Fly(loudness);
        }
    }
}
