using System.Collections;
using System.Collections.Generic;
using System.IO;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VoiceEmotionManager : MonoBehaviour

{
    [SerializeField]
    private AudioSource AudioSource;

    [SerializeField] 
    private Slider ConfidenceSlider;

    [SerializeField] 
    private Slider PleasentSlider;
    private List<Object> audios;
    
    private int currentIndex;
    private string folderPath = "audios/voiceEmotion";
    private List<VoiceEmotionAnswer> answers;
    [SerializeField]
    private int selectedEmotion;
    [SerializeField] private Text DebugText;

    private void Start()
    {
        audios = new List<Object>();
        audios.AddRange(Resources.LoadAll(folderPath,typeof(AudioClip)));
        currentIndex = 0;
        answers = new List<VoiceEmotionAnswer>();
        StartCoroutine(SetAudioSource());
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.LoadLevel(0);
        }
    }
    public void GoNextSound()
    {
        answers.Add(new VoiceEmotionAnswer()
        {
            Emotion = selectedEmotion,
            Confidence = (int)ConfidenceSlider.value,
            Pleasent = (int)PleasentSlider.value
        });
        ConfidenceSlider.value = 50;
        PleasentSlider.value = 50;
        currentIndex++;
        if (audios.Count > currentIndex)
            StartCoroutine(SetAudioSource());
        else
            Application.LoadLevel(0);
    }

    IEnumerator  SetAudioSource()
    {
        yield return new WaitForSeconds(1);
        // Set the clip to the audio source
        //AudioSource.clip = Resources.Load<AudioClip>(filePath);
        AudioSource.clip = (AudioClip)audios[currentIndex];
        AudioSource.Play();
    }
    

    public void SetEmotion(int number)
    {
        selectedEmotion = number;
    }
}
