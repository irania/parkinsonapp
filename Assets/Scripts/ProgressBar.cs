using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] 
    private GameObject CirclePrefab;

    [SerializeField] 
    private GameObject[] Circles;

    [SerializeField] 
    private Sprite FillCircle;

    [SerializeField] 
    private int StepsNumber;

    private int index;

    private void Start()
    {
        index = 0;
        GenerateCircles();
    }

    private void GenerateCircles()
    {
        
    }
    
    public void GoNext()
    {
        index++;
        if(index<Circles.Length)
            Circles[index].GetComponent<SpriteRenderer>().sprite = FillCircle;
    }
    
}
