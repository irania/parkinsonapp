using System.Collections;
using System.Collections.Generic;
using System.IO;
using Entities;
using UnityEngine;
using UnityEngine.UI;

public class VoiceEmotionManager : MonoBehaviour

{
    [SerializeField]
    private AudioSource AudioSource;

    [SerializeField] 
    private Slider ConfidenceSlider;

    [SerializeField] 
    private Slider PleasentSlider;
    [SerializeField]
    private List<string> audioFiles;
    
    private int currentIndex;
    private string folderPath = "assets/audios/voiceEmotion";
    private List<VoiceEmotionAnswer> answers;
    [SerializeField]
    private int selectedEmotion;

    private void Start()
    {
        string[] fileEntries = Directory.GetFiles(folderPath, "*.wav", SearchOption.AllDirectories);
        audioFiles.AddRange(fileEntries);
        currentIndex = 0;
        answers = new List<VoiceEmotionAnswer>();

    }

    public void GoNextSound()
    {
        answers.Add(new VoiceEmotionAnswer()
        {
            Emotion = selectedEmotion,
            Confidence = (int)ConfidenceSlider.value,
            Pleasent = (int)PleasentSlider.value
        });
        currentIndex++;
        if (audioFiles.Count < currentIndex)
            SetAudioSource();
        else
            Application.LoadLevel(0);
    }

    private void SetAudioSource()
    {
        
        string filePath = Path.Combine(folderPath,audioFiles[currentIndex]);

        // Set the clip to the audio source
        AudioSource.clip = Resources.Load<AudioClip>(filePath);;
        AudioSource.Play();
    }

    public void SetEmotion(int number)
    {
        selectedEmotion = number;
    }
}
