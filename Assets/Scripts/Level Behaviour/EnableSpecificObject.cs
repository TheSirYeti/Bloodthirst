using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpecificObject : MonoBehaviour
{
    public GameObject item;

    public void EnableGameObject()
    {
        item.SetActive(true);
    }

    public void DisableGameObject()
    {
        item.SetActive(false);
    }
}
