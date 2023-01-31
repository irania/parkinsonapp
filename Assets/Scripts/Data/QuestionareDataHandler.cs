using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class QuestionareDataHandler: Singleton<QuestionareDataHandler>
    {
        private GameData GameData;
        private IList<VoiceQuestionsAnswer> answers;

        private void Start()
        {
            answers = new List<VoiceQuestionsAnswer>();
        }

        public void SendData()
        {
            GameData = CreateGameData();
            SendDataManager.Instance.SendJsonData(GameData);
        }

        public void AddAnswer(int answer, int confidence, int pleasent)
        {
            answers.Add(new VoiceQuestionsAnswer
            {
                questionNumber = answers.Count,
                answer = answer,
                confidence = confidence,
                pleasent = pleasent
            });
        }
        public void AddAnswer(int answer)
        {
            answers.Add(new VoiceQuestionsAnswer
            {
                questionNumber = answers.Count,
                answer = answer,
            });
        }

        private GameData CreateGameData()
        {
            var gameData = new GameData();

            gameData.DataName = "answers";
            
            gameData.Data = JsonHelper.ToJson(answers.ToArray());

            return gameData;


        }
    }

    [Serializable]
    internal class VoiceQuestionsAnswer
    {
        public int questionNumber;
        public int answer;
        public int confidence;
        public int pleasent;
    }
}