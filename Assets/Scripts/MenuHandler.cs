using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] 
    private Text UserNameText;
    private void Update()
    {
        UserNameText.text = "Hi! " + DataManager.Instance.Users[DataManager.Instance.CurrentUser].UserName;
    }

    public void LoadHandwritingScene()
    {
        Application.LoadLevel ("DrawingScene");
    }
    public void LoadSelfieScene()
    {
        Application.LoadLevel ("TakingSelfieScene");
    }
    public void LoadVisualEmotionScene()
    {
        Application.LoadLevel ("VisualEmotionScene");
    }
    public void LoadVoicsEmotionScene()
    {
        Application.LoadLevel ("VoiceEmotionScene");
    }
    
    public void LoadQuestionareScene()
    {
        Application.LoadLevel ("QuestionareScene");
    }

    public void OpenUserPanel()
    {
        
    }
}
