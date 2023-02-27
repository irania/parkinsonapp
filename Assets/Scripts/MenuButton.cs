using System;
using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;
 
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
 
    private Text theText;
    [SerializeField]
    private SpriteRenderer theIcon;
    [SerializeField]
    private SpriteRenderer theSquare;
    private Color highlitedColor;
    private Color normalColor;

    private void Awake()
    {
        theText = GetComponentInChildren<Text>();
        highlitedColor = GetComponent<Button>().colors.highlightedColor;
        highlitedColor = GetComponent<Button>().colors.normalColor;
        theIcon.color = highlitedColor;
        theText.color = highlitedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.white; 
        theIcon.color = Color.white; 
        theSquare.color = highlitedColor; 
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = highlitedColor;
        theIcon.color = highlitedColor;
        theSquare.color = normalColor; 
        
    }
    
}