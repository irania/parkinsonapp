using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

public class TakeSelfieManager : MonoBehaviour
{
    [FormerlySerializedAs("EmotionsVideoFiles")] [SerializeField] 
    private List<Object> emotionsVideoFiles;
    [SerializeField]
    private VideoPlayer EmotionPlayer;
    private int emotionIndex;
    private string folderPath = "videos";
    
    
    private void Start()
    {
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
        if (emotionIndex >= emotionsVideoFiles.Count)
            GoHome();
        else
        {
            StartCoroutine(SetVideo());
        }
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
