using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSelfieManager : MonoBehaviour
{
    [SerializeField] 
    private List<Sprite> Emotions;
    [SerializeField]
    private SpriteRenderer Emotion;
    private int emotionIndex;
    
    private void Start()
    {
        emotionIndex = 0;
        SetSprite();
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
        if (emotionIndex >= Emotions.Count)
            GoHome();
        else
        {
            SetSprite();
        }
    }

    private void GoHome()
    {
        Application.LoadLevel(0);
    }

    private void SetSprite()
    {
        Emotion.sprite = Emotions[emotionIndex];
    }
}
