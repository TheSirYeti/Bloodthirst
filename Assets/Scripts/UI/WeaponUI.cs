using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public List<Image> weaponImages;
    public List<Image> specialImages;

    private void Start()
    {
        EventManager.UnSubscribe("SetWeaponImageUI", SetImageUI);
        EventManager.Subscribe("SetWeaponImageUI", SetImageUI);
    }

    public void SetImageUI(object[] parameters)
    {
        for(int i = 0; i < weaponImages.Count; i++)
        {
            weaponImages[i].enabled = false;
            specialImages[i].enabled = false;
        }

        weaponImages[(int)parameters[0]].enabled = true;
        specialImages[(int)parameters[0]].enabled = true;
    }
}
