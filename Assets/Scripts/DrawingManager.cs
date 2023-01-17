using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    private void SetPattern()
    {
        FirstPattern.sprite = Patterns[patternIndex];
        SecondPattern.sprite = null;
        SymmetricPattern.sprite = null;
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
        if (patternIndex== Patterns.Count)
        {
            Application.LoadLevel ("MainMenuScene");
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
