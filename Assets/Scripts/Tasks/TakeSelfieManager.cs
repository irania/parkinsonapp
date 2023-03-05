using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public class TakeSelfieManager : MonoBehaviour
{
    private const int GameId = 1;
    [FormerlySerializedAs("EmotionsVideoFiles")] [SerializeField] 
    private List<Object> emotionsVideoFiles;
    [SerializeField]
    private VideoPlayer EmotionPlayer;
    private int emotionIndex;
    private string folderPath = "videos/avatar";
    private ScreenShotHandler ScreenShotHandler;
    
    private void Start()
    {
        ScreenShotHandler = gameObject.GetComponent<ScreenShotHandler>();
        emotionsVideoFiles = new List<Object>();
        emotionsVideoFiles.AddRange(Resources.LoadAll(folderPath,typeof(VideoClip)));
        emotionIndex = 0;
        StartCoroutine(SetVideo());
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.LoadLevel(0);
        }
    }
    
    
    public void MoveNext()
    {
        emotionIndex++;
        StartCoroutine(SendScreenShotAndNext());
    }

    public void BackButtonClick()
    {
        if(emotionIndex<=0)
            GoHome();
        else
        {
            emotionIndex--;
            StartCoroutine(SetVideo());
        }
    }
    public void NextButtonClick()
    {
        emotionIndex++;
        if(emotionIndex>= emotionsVideoFiles.Count)
            GoHome();
        else
        {
            
            StartCoroutine(SetVideo());
        }
    }
    private IEnumerator SendScreenShotAndNext()
    {
        string fileName = "emo" + emotionIndex + "-" + DateTime.Now.Ticks+".png";
        StartCoroutine(ScreenShotHandler.TakeScreenShotAndSave(fileName));
        yield return new WaitForSeconds(2);
        CameraHandler.Instance.TakePicture();
        SendDataManager.Instance.SendImageFile(fileName);
        if (emotionIndex >= emotionsVideoFiles.Count)
        {
            DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
            GoHome();
        }
        else
        {
            StartCoroutine(SetVideo());
        }
        yield return new WaitForSeconds(1);
    }

    private void GoHome()
    {
        Application.LoadLevel(0);
    }

    IEnumerator SetVideo()
    {
        // Set the clip to the audio source
        EmotionPlayer.clip = (VideoClip)emotionsVideoFiles[emotionIndex];
        yield return new WaitForSeconds(1);
        EmotionPlayer.Play();
    }
    
    
}
