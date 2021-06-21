using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public Collider collider;
    
    public void enableCollider()
    {
        collider.enabled = true;
    }

    public void disableCollider()
    {
        collider.enabled = false;
    }
}
