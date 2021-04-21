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

    private bool airAttackCooldown;

    private void Update()
    {
        //movement.restrictMovement();
        if (Input.GetButtonDown(jumpButtonName))
        {
            movement.Jump();
        }
        basicAttacks.checkCombo();
        if (Input.GetButtonDown(attackButtonName))
        {
            if (movement.groundCheck.getStatus() > 0) { 
                basicAttacks.attack(); 
                playerVFX.enableVFX(0); 
            }
            else if(basicAttacks.checkAirAttackCooldown()){ 
                basicAttacks.airAttack(); 
                movement.toggleFloat(); 
                playerVFX.enableVFX(1); 
            }
        }
    }

    private void FixedUpdate()
    {
        movement.Move();

    }
}
