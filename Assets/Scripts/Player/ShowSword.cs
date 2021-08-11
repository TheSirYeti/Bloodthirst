using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSword : MonoBehaviour
{
    private void Start()
    {
        EventManager.UnSubscribe("ShowSword", Show);
        EventManager.Subscribe("ShowSword", Show);
    }
    public void Show(object[] parameters)
    {
        GetComponent<Renderer>().enabled = true;
    }
}
