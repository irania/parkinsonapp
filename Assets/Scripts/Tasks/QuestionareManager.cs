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
        [FormerlySerializedAs("TextQuestion")] [SerializeField]
        private string[] TextQuestions;

        [SerializeField] private GameObject[] FaTextQuestions;
        [FormerlySerializedAs("VoiceQuestion")] [SerializeField]
        private AudioClip[] VoiceQuestions;
        
        private int currentIndex;
        [SerializeField]
        private RateAnswerScript RateAnswer;

        private void Start()
        {
            currentIndex = 0;
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
                foreach (var question in FaTextQuestions)
                {
                    question.SetActive(false);
                }
                FaTextQuestions[currentIndex].SetActive(true);
//                AudioSource.clip = VoiceQuestions[currentIndex];
                //AudioSource.Play();
            }

            RateAnswer.SetNull();
        }

        public void NextButtonClick()
        {
            QuestionareDataHandler.Instance.AddAnswer(RateAnswer.Value);
            currentIndex++;
            if (TextQuestions.Length > currentIndex)
            {
                SetQuestion();
            }
            else
            {
                QuestionareDataHandler.Instance.SendData();
                Application.LoadLevel(0);
            }
        }
        
    }
}