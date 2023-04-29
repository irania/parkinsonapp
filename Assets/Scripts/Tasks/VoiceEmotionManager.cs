using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VoiceEmotionManager : MonoBehaviour
{
    private const int GameId = 2;
    [SerializeField]
    private AudioSource AudioSource;

    [SerializeField] 
    private Slider ConfidenceSlider;

    [SerializeField] 
    private Slider PleasentSlider;
    private List<Object> audios;
    
    private int currentIndex;
    private string folderPath = "audios/voiceEmotion";
    [SerializeField]
    private int selectedEmotion;
    [SerializeField] private Text DebugText;

    [SerializeField] private Toggle[] EmotionButtons;

    private void Start()
    {
        audios = new List<Object>();
        audios.AddRange(Resources.LoadAll(folderPath,typeof(AudioClip)));
        currentIndex = 0;
        StartCoroutine(SetAudioSource());
        selectedEmotion = 0;
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            GoHome();
        }
        if(selectedEmotion>=0)
            EmotionButtons[selectedEmotion].Select();
    }
    public void GoNextSound(bool isAnswered)
    {
        QuestionareDataHandler.Instance.AddAnswer(isAnswered?selectedEmotion:-1,(int)ConfidenceSlider.value,(int)PleasentSlider.value);
        ConfidenceSlider.value = 50;
        PleasentSlider.value = 50;
        currentIndex++;
        selectedEmotion = 0;
        if (audios.Count > currentIndex)
            StartCoroutine(SetAudioSource());
        else
        {
            DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
            QuestionareDataHandler.Instance.SendData();
            GoHome();
        }

        
    }

    IEnumerator  SetAudioSource()
    {
        yield return new WaitForSeconds(1);
        PlayAudio();
    }

    public void PlayAudio()
    {
        AudioSource.clip = (AudioClip)audios[currentIndex];
        AudioSource.Play();
    }

    public void SetEmotion(int number)
    {
        selectedEmotion = number;
    }

    public void GoHome()
    {
        Application.LoadLevel(0);
    }
    
}
