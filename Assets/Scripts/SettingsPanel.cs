using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private Slider SensitivitySlider;

    [SerializeField] 
    private GameObject settingPanelGameObject;

    [SerializeField] private Image ButtonImage;

    [SerializeField] private Sprite RecordSprite;

    [SerializeField] private Sprite StopSprite;
    

    public bool IsRecording;

    public double SumLoudness;

    public int NumberOfRecords;

    public double maxVoice;
    // Start is called before the first frame update
    void Start()
    {
        SensitivitySlider.value = (float)Settings.Instance.sensitivity;
    }

    private void Update()
    {
        if (IsRecording)
        {
            SumLoudness += AudioLoudnessDetector.Instance.GetLoudnessFromMicrophone();
            NumberOfRecords++;
            var newValue = (float) SumLoudness / NumberOfRecords;
            maxVoice = Math.Max(newValue, maxVoice);
            SensitivitySlider.value = (float)Settings.Instance.defaultSensitivity*3 / (float)(maxVoice / Settings.Instance.walkThreshold);
        }
    }

    public void SaveClick()
    {
        Settings.Instance.SetParameters(SensitivitySlider.value);
        settingPanelGameObject.SetActive(false);
    }

    public void ClickOnSetSensitivity()
    {
        if(!IsRecording)
            StartRecord();
        else
            StopRecord();
    }
    void StartRecord()
    {
        ButtonImage.sprite = StopSprite;
        IsRecording = true;
        SumLoudness = 0;
        NumberOfRecords = 1;
        maxVoice = 0;
    }

    void StopRecord()
    {
        ButtonImage.sprite = RecordSprite;
        IsRecording = false;
    }
    

}
