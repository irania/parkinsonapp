using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public Slider mySlider; // The Slider component
    public Text valueText; // The Text UI element

    void Update() {
        // Get the current value of the slider
        float sliderValue = mySlider.value;

        // Set the text of the Text UI element to the current value of the slider
        valueText.text = sliderValue.ToString()+"%";
        
    }
}
