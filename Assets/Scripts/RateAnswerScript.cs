using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class RateAnswerScript : MonoBehaviour
{
    public int Value=1;
    [SerializeField]
    private Sprite FillSprite;
    [SerializeField]
    private Sprite EmptySprite;

    [SerializeField] 
    private List<Image> Circles;

    private void Start()
    {
        SetValue(2);
    }

    public void OnClick(int number)
    {
        SetValue(number);
    }

    public void SetValue(int number)
    {
        Value = number;
        for(int i = 0; i < Circles.Count; i++)
        {
            Circles[i].sprite = EmptySprite;
        }
        Circles[number].sprite = FillSprite;
    }
    
}
