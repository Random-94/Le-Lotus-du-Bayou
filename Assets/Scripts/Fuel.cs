using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private Gradient gradient;

    public void SetMaxFuel(float fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetFuel(float fuel)
    {
        slider.value = fuel;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
