using System;
using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;
 
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
 
    private Text theText;
    private SpriteRenderer theIcon;
    private Color highlitedColor;

    private void Awake()
    {
        theText = GetComponentInChildren<Text>();
        theIcon = GetComponentInChildren<SpriteRenderer>();
        highlitedColor = GetComponent<Button>().colors.highlightedColor;
        theIcon.color = highlitedColor;
        theText.color = highlitedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.white; //Or however you do your color
        theIcon.color = Color.white; //Or however you do your color
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = highlitedColor; //Or however you do your color
        theIcon.color = highlitedColor; //Or however you do your color
        
    }
    
}