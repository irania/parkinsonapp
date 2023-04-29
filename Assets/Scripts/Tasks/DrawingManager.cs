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
    private const int GameId = 0;
    [FormerlySerializedAs("FirstPattern")] [SerializeField] 
    private SpriteRenderer Pattern;
    [SerializeField] 
    private GameObject RightPattern;
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

    [SerializeField] private GameObject EnvCanvas;

    [SerializeField]
    private RTLTextMeshPro FaInstructionText;
    [SerializeField] private Button NextButton;

    private ScreenShotHandler ScreenShotHandler;

    private int patternIndex;

    private bool isDrawing;
    // Start is called before the first frame update
    void Awake()
    {
        ScreenShotHandler = gameObject.GetComponent<ScreenShotHandler>();
        patternIndex = 0;
        isDrawing = false;
        SetPattern();
        
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            GoHome();
        }
    }

    private void SetPattern()
    {
        InstructionText.text = InstructionTexts[patternIndex];
        FaInstructionText.text = FaInstructionTexts[patternIndex];
        Pattern.sprite = Patterns[patternIndex];
        if (patternIndex >= Patterns.Count - 2)
        {
            RightPattern.SetActive(true);
            Pattern.enabled = false;
        }
    }

    public void NextButtonClick()
    {
        patternIndex++;
        if (patternIndex== Patterns.Count)
        {
            DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
            GoHome();
        }
        else
        {
            CleanPage();
            SetPattern();
        }
    }
    public void PrevButtonClick()
    {
        
        if (patternIndex>0)
        {
            patternIndex--;
            CleanPage();
            SetPattern();
        }
        else
        {
            GoHome();
        }
    }

    public void DoneButton()
    {
        patternIndex++;
        StartCoroutine(GoNext());
    }

    IEnumerator GoNext()
    {
        
        var fileName = "dr" + patternIndex + "-" + DateTime.Now.Ticks + ".png";
        EnvCanvas.SetActive(false);
        StartCoroutine(ScreenShotHandler.TakeScreenShotAndSave(fileName));
        yield return new WaitForSeconds(2);
        try {
            EnvCanvas.SetActive(true); 
            SendDataManager.Instance.SendImageFile(fileName);
        }catch{}

        if (patternIndex== Patterns.Count)
        {
            DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
            GoHome();
        }
        else
        {
            isDrawing = false;
            SetPattern();
            CleanPage();

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
    
    public void GoHome()
    {
        Application.LoadLevel(0);
    }
}
