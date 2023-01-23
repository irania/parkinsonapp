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
    private string folderPath = "assets/resources/audios/voiceEmotion";
    private List<VoiceEmotionAnswer> answers;
    [SerializeField]
    private int selectedEmotion;

    private void Start()
    {
        string[] fileEntries = Directory.GetFiles(folderPath, "*.wav", SearchOption.AllDirectories);
        audioFiles.AddRange(fileEntries);
        currentIndex = 0;
        answers = new List<VoiceEmotionAnswer>();
        StartCoroutine(SetAudioSource());
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
        Debug.Log(audioFiles.Count);
        Debug.Log(currentIndex);
        if (audioFiles.Count > currentIndex)
            StartCoroutine(SetAudioSource());
        else
            Application.LoadLevel(0);
    }

    IEnumerator  SetAudioSource()
    {
        string filePath = audioFiles[currentIndex].Remove(0,17).Split('.')[0].Replace('\\','/');
        yield return new WaitForSeconds(1);
        // Set the clip to the audio source
        AudioSource.clip = Resources.Load<AudioClip>(filePath);
        AudioSource.Play();
    }
    

    public void SetEmotion(int number)
    {
        selectedEmotion = number;
    }
}
