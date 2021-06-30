using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]Image HPSlider;

    private void Start()
    {
        EventManager.Subscribe("SetHP", SetHPValue);
        EventManager.Subscribe("KillPlayer", NoMoreHP);
    }

    public void SetHPValue(object[] parameters)
    {
        HPSlider.fillAmount = (float)parameters[0];
    }

    public void NoMoreHP(object[] parameters)
    {
        HPSlider.fillAmount = 0;
    }
}
