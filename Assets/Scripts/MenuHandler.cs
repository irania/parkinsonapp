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
    public float displayTime = 0.5f;
    [SerializeField]
    public GameObject orangePanel;
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
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("DrawingScene"));
    }
    public void LoadSelfieScene()
    {
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("TakingSelfieScene"));
    }
    public void LoadVisualEmotionScene()
    {
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("VisualEmotionScene"));
    }
    public void LoadVoicsEmotionScene()
    {
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("VoiceEmotionScene"));
    }
    
    public void LoadQuestionareScene()
    {
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("QuestionareScene"));
    }
    public void LoadProfile()
    {
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("ProfileScene"));
    }
    public void LoadPhysical()
    {
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("PhysicalScene"));
    }
    
    public void LoadVoiceGame()
    {
        StartCoroutine(ShowOrangeScreenAndLoadNextScene("MainMenu"));
    }
    public void OpenUserPanel()
    {
        
    }
    
    IEnumerator ShowOrangeScreenAndLoadNextScene(string nextSceneName)
    {
        orangePanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        
        Application.LoadLevel(nextSceneName);
        //orangePanel.gameObject.SetActive(false);
    }
}
