using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class RateAnswerScript : MonoBehaviour
{
    public int Value=1;

    [FormerlySerializedAs("Circles")] [SerializeField] 
    private List<Image> Buttons;

    [SerializeField] private Color _color;

    private void Start()
    {   
        SetNull();
    }

    public void OnClick(int number)
    {
        SetValue(number);
    }

    public void SetValue(int number)
    {
        Value = number;
        for(int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].color = Color.white;
        }

        Buttons[number].color = _color;
    }

    public void SetNull()
    {
        Value = -1;
        for(int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].color = Color.white;
        }
    }
    
}
