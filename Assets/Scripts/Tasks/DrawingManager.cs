using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject DrawingCanvas;
    [SerializeField] 
    private GameObject PatternCanvas;
    [SerializeField] 
    private SpriteRenderer FirstPattern;
    [SerializeField] 
    private SpriteRenderer SecondPattern;
    [SerializeField] 
    private SpriteRenderer SymmetricPattern;
    [SerializeField]
    private List<Sprite> Patterns;

    [SerializeField] 
    private string[] InstructionTexts;

    [SerializeField] 
    private Text InstructionText;
    [SerializeField] 
    private Text debugText;

    private int patternIndex;

    private bool isDrawing;
    // Start is called before the first frame update
    void Awake()
    {
        PatternCanvas.SetActive(true);
        DrawingCanvas.SetActive(false);
        patternIndex = 0;
        isDrawing = false;
        SetPattern();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.LoadLevel(0);
        }
    }

    private void SetPattern()
    {
        FirstPattern.sprite = Patterns[patternIndex];
        SecondPattern.sprite = null;
        SymmetricPattern.sprite = null;
        InstructionText.text = InstructionTexts[patternIndex];
        if (patternIndex > 5)
            SymmetricPattern.sprite = Patterns[patternIndex];
        else
        {
            SecondPattern.sprite = Patterns[patternIndex];
        }
    }
    public void LetsGoButton()
    {
        DrawingCanvas.SetActive(true);
        PatternCanvas.SetActive(false);
        isDrawing = true;
    }

    public void DoneButton()
    {
        patternIndex++;
        StartCoroutine(GoNext());


    }

    IEnumerator GoNext()
    {
        var fileName = "dr" + patternIndex +"-"+DateTime.Now.Ticks+".png";
        StartCoroutine(TakeScreenShotAndSave(fileName));
        yield return new WaitForSeconds(2);
        //SendDataManager.Instance.SendFile(Application.persistentDataPath+"/"+fileName);
        debugText.text = Application.persistentDataPath + "/" + fileName;
        if (patternIndex== Patterns.Count)
        {
            Application.LoadLevel (0);
        }
        else
        {
            isDrawing = false;
            SetPattern();
            CleanPage();
            DrawingCanvas.SetActive(false);
            PatternCanvas.SetActive(true);
        }

    }

    private IEnumerator TakeScreenShotAndSave(string filename)
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();
        
        string path = Application.persistentDataPath + "/" + filename;
        System.IO.File.WriteAllBytes(path, screenshot.EncodeToPNG());

        // Save the screenshot to the device's photo gallery
        AndroidJavaClass classUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classMedia = new AndroidJavaClass("android.media.MediaScannerConnection");
        classMedia.CallStatic("scanFile", objActivity, new string[] { path }, null, new AndroidJavaProxy("android.media.MediaScannerConnection.OnScanCompletedListener"));
        
    }

    public void CleanPage()
    {
        var lines = GameObject.FindGameObjectsWithTag("Line");
        foreach (var line in lines)
        {
            Destroy(line);
        }
    }
    
}
