using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using RTLTMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject InstructionCanvas;
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
    private string[] FaInstructionTexts;

    [SerializeField] 
    private Text InstructionText;
    [SerializeField] 
    private Text debugText;

    [SerializeField]
    private RTLTextMeshPro FaInstructionText;

    [SerializeField] private Vector3[] StartPositions;
    [SerializeField] private GameObject StartPoint;
    [SerializeField] private GameObject Pencil;
    [SerializeField] private Button NextButton;

    private ScreenShotHandler ScreenShotHandler;

    private int patternIndex;

    private bool isDrawing;
    // Start is called before the first frame update
    void Awake()
    {
        ScreenShotHandler = gameObject.GetComponent<ScreenShotHandler>();
        PatternCanvas.SetActive(false);
        DrawingCanvas.SetActive(false);
        InstructionCanvas.SetActive(true);
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
        FaInstructionText.text = FaInstructionTexts[patternIndex];
        StartPoint.transform.localPosition = StartPositions[patternIndex];
        Pencil.transform.localPosition = StartPositions[patternIndex]+new Vector3(-10,10,0);
        if (patternIndex > 8)
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
        NextButton.interactable = false;
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
        NextButton.interactable = true;

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
