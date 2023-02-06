using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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

    private ScreenShotHandler ScreenShotHandler;

    private int patternIndex;

    private bool isDrawing;
    // Start is called before the first frame update
    void Awake()
    {
        ScreenShotHandler = gameObject.GetComponent<ScreenShotHandler>();
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
        StartCoroutine(ScreenShotHandler.TakeScreenShotAndSave(fileName));
        yield return new WaitForSeconds(2);
        SendDataManager.Instance.SendImageFile(fileName);
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

    public void CleanPage()
    {
        var lines = GameObject.FindGameObjectsWithTag("Line");
        foreach (var line in lines)
        {
            Destroy(line);
        }
    }
    
}
