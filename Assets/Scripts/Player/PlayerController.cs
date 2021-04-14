using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Behaviour;
using Player.Animations;

public class PlayerController : MonoBehaviour
{
    public Movement movement;
    public BasicAttacks basicAttacks;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButtonName = "Jump";
    public string attackButtonName = "Fire1";

    private void Update()
    {
        movement.restrictMovement();
        if (Input.GetButtonDown(jumpButtonName))
        {
            movement.Jump();
        }
        basicAttacks.checkCombo();
        if (Input.GetButtonDown(attackButtonName))
        {
            basicAttacks.attack();
        }
    }

    private void FixedUpdate()
    {
        movement.Move();
    }
}
