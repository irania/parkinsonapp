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
        private const int GameId = 3;
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
                DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
                QuestionareDataHandler.Instance.SendData();
                Application.LoadLevel(0);
            }
        }

        public void PrevButtonClick()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                SetQuestion();
            }
            else
            {
                Application.LoadLevel(0);
            }
        }
        
    }
}