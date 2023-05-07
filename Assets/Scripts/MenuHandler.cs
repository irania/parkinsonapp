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
    [SerializeField]
    private List<GameObject> ButtonsTick;

    private void Start()
    {
        SetTicks();
    }

    public void SetTicks()
    {
        for (int i=0;i< ButtonsTick.Count;i++)
        {
            ButtonsTick[i].SetActive(DataManager.Instance.GetCurrentUser().LevelsDone[i]);
        }
    }

    private void Update()
    {
        //todo it has bug
        if (DataManager.Instance.GetCurrentUser() is null)
            UserNameText.text = "Please create an account";
        else
            UserNameText.text = DataManager.Instance.GetCurrentUser().UserName;
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
    public void LoadProfile()
    {
        Application.LoadLevel ("ProfileScene");
    }
    public void LoadPhysical()
    {
        Application.LoadLevel ("PhysicalScene");
    }
    
    public void LoadVoiceGame()
    {
        Application.LoadLevel ("MainMenu");
    }
    public void OpenUserPanel()
    {
        
    }
}
