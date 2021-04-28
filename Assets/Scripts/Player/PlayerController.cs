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
        if (Input.GetButtonDown(jumpButtonName))
        {
            movement.Jump();
        }
        if (Input.GetButtonDown(attackButtonName) && basicAttacks.checkAttackCooldown())
        {
            if (movement.groundCheck.getStatus() > 0) { 
                basicAttacks.attack();
                //basicAttacks.checkCombo();
                playerVFX.enableGroundVFX(basicAttacks.getCurrentAttackTurn());
                //movement.restrictMovement();
            }
            else { 
                basicAttacks.airAttack(); 
                movement.toggleFloat();
                playerVFX.enableAirVFX(0); 
            }
        }
        basicAttacks.resetAttackTurn();
    }

    private void FixedUpdate()
    {
        movement.Move();
    }
}
