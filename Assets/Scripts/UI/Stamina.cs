using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider staminaSlider;
    public Gradient gradient;
    public Image StaminaBar;

    public void SetMaxStaminaSlider(float value)
    {
        staminaSlider.maxValue = value; //set maxHP in script instead of inspector
        staminaSlider.value = value;  // set player's hp

        gradient.Evaluate(1f); // set color for different Hp %
    }

    public void setStaminaSlider(float value)
    {
        staminaSlider.value = value;  //update player's health

        StaminaBar.color = gradient.Evaluate(staminaSlider.normalizedValue); //set the HPBar color match with the gradient we've set
    }
}
