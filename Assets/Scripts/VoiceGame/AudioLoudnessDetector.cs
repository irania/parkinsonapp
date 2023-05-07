using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioLoudnessDetector : MonoBehaviour
{

    public static AudioLoudnessDetector Instance;
    private int sampleWindow;

    private AudioClip microphoneClip;

    [SerializeField]
    private string microphoneName;

    public float loudness;
    
    // Start is called before the first frame update
    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        sampleWindow = Settings.Instance.sampleWindow;
        MicrophoneToAudioClip();
    }

    void MakeInstance(){
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void MicrophoneToAudioClip()
    {
        //Get the last microphone in device list
        foreach (var mic in Microphone.devices)
        {
            microphoneName = mic;
            #if UNITY_EDITOR
                break;
            #endif
        }
        
        microphoneClip = Microphone.Start(microphoneName, true, 5, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        loudness =  GetLoudnessFromAudioClip(Microphone.GetPosition(microphoneName), microphoneClip);
        return loudness;
    }
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;
        if (startPosition < 0) return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);
        
        //compute loudness
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow; 
    }
}
