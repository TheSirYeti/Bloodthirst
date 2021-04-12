using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundAnimatorCheck : MonoBehaviour
{
    public Animator animator;
    public string boolParameterName = "IsOnGround";
    private int currentColliderStatus;

    private void OnTriggerEnter(Collider other)
    {
        currentColliderStatus++;
        if (currentColliderStatus > 0)
        {
            animator.SetBool(boolParameterName, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentColliderStatus--;
        if (currentColliderStatus < 1)
        {
            animator.SetBool(boolParameterName, false);
        }
    }
}
