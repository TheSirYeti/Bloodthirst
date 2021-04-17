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
            if (movement.groundCheck.getStatus() > 0) basicAttacks.attack();
            else basicAttacks.airAttack();
        }
    }

    private void FixedUpdate()
    {
        movement.Move();
    }
}
