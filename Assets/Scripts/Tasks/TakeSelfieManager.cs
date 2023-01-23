using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class TakeSelfieManager : MonoBehaviour
{
    [FormerlySerializedAs("EmotionsVideoFiles")] [SerializeField] 
    private List<string> emotionsVideoFiles;
    [SerializeField]
    private VideoPlayer EmotionPlayer;
    private int emotionIndex;
    private string folderPath = "assets/resources/videos";
    
    private void Start()
    {
        string[] files = Directory.GetFiles(folderPath, "*.wav", SearchOption.AllDirectories);
        emotionsVideoFiles.AddRange(files);
        emotionIndex = 0;
        StartCoroutine(SetVideo());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        string filePath = emotionsVideoFiles[emotionIndex].Remove(0,17).Split('.')[0].Replace('\\','/');
        // Set the clip to the audio source
        EmotionPlayer.clip = Resources.Load<VideoClip>(filePath);
        yield return new WaitForSeconds(1);
        EmotionPlayer.Play();
        
        
    }
}
