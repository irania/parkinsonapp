using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{

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

    public void OpenUserPanel()
    {
        
    }
}
