using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace.Tasks
{
    public class QuestionareManager: MonoBehaviour
    {
        [SerializeField] 
        private Text QuestionText;
        [SerializeField] 
        private AudioSource AudioSource;
        [SerializeField] 
        private Slider AnswerSlider;
        [FormerlySerializedAs("TextQuestion")] [SerializeField]
        private string[] TextQuestions;
        [FormerlySerializedAs("VoiceQuestion")] [SerializeField]
        private AudioClip[] VoiceQuestions;
        private int currentIndex;

        private List<int> answers;

        private void Start()
        {
            currentIndex = 0;
            answers = new List<int>();
            SetQuestion();
        }
        private void Update()
        {
            if (Keyboard.current.escapeKey.isPressed)
            {
                Application.LoadLevel(0);
            }
        }
        private void SetQuestion()
        {
            if (currentIndex < TextQuestions.Length)
            {
                QuestionText.text = TextQuestions[currentIndex];
                AudioSource.clip = VoiceQuestions[currentIndex];
                AudioSource.Play();
            }
        }

        public void NextButtonClick()
        {
            Debug.Log(answers);
            answers.Add((int)AnswerSlider.value);
            currentIndex++;
            if (TextQuestions.Length > currentIndex)
                SetQuestion();
            else
                Application.LoadLevel(0);
        }
        
    }
}