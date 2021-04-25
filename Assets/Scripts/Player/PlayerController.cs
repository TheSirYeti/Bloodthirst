using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Behaviour;
using Player.Animations;
using VFX.Player;

public class PlayerController : MonoBehaviour
{
    public Movement movement;
    public BasicAttacks basicAttacks;
    public swordEffects swordEffects;
    public PlayerVFX playerVFX;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButtonName = "Jump";
    public string attackButtonName = "Fire1";

    private void Update()
    {
        //movement.restrictMovement();
        if (Input.GetButtonDown(jumpButtonName))
        {
            movement.Jump();
        }

        if (Input.GetButtonDown(attackButtonName) && basicAttacks.checkAttackCooldown())
        {
            if (movement.groundCheck.getStatus() > 0) { 
                basicAttacks.attack(); 
                playerVFX.enableGroundVFX(basicAttacks.getCurrentAttackTurn() - 1); 
            }
            else { 
                basicAttacks.airAttack(); 
                movement.toggleFloat(); 
                playerVFX.enableAirVFX(0); 
            }
        }
        basicAttacks.checkCombo();
    }

    private void FixedUpdate()
    {
        movement.Move();

    }
}
