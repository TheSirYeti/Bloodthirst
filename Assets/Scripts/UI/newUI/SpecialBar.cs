using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    [SerializeField] Image SpecialSlider;

    private void Start()
    {
        EventManager.Subscribe("AddSpecial", SetSpecialValue);
        EventManager.Subscribe("ResetSpecial", ResetSpecialValue);
    }

    public void SetSpecialValue(object[] parameters)
    {
        SpecialSlider.fillAmount += (float)parameters[0];
    }

    public void ResetSpecialValue(object[] parameters)
    {
        SpecialSlider.fillAmount = 0;
    }

    public float GetSpecialValue()
    {
        return SpecialSlider.fillAmount;
    }
}
