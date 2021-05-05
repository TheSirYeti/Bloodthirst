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
    public Cinemachine.CinemachineFreeLook vcamera;
    public CameraLock lockSystem;
    public Crosshair crosshair;

    public Transform center;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButtonName = "Jump";
    public string attackButtonName = "Fire1";
    public string lockButtonName = "LockButton";
    public string toggleLockName = "ToggleLock";

    private void Update()
    {
        if (Input.GetButtonDown(jumpButtonName))
        {
            if(movement.groundCheck.getStatus() > 0)
            {
                movement.Jump();
            } else if(basicAttacks.checkAttackCooldown())
            {
                basicAttacks.airAttack();
                movement.toggleFloat();
                playerVFX.enableAirVFX(0);
                movement.StartCoroutine(movement.stopRotation());
            }
        }
        if (Input.GetButtonDown(attackButtonName) && basicAttacks.checkAttackCooldown())
        {
            if (movement.groundCheck.getStatus() > 0) { 
                basicAttacks.attack();
                //basicAttacks.checkCombo();
                playerVFX.enableGroundVFX(basicAttacks.getCurrentAttackTurn());
                movement.restrictMovement();
            }
        }
        basicAttacks.resetAttackTurn();
    }

    private void FixedUpdate()
    {
        movement.Move();

        if (Input.GetButtonDown(lockButtonName))
        {
            if(lockSystem.isLocked == false)
            {
                lockSystem.isLocked = true;
                lockSystem.index = 0;
                if (lockSystem.checkEnemiesAround() && checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) >= 2f)
                {
                    Vector3 centerPosition = (lockSystem.currentEnemy().GetComponent<Enemy>().lookAtPoint.transform.position + transform.position) / 2;
                    crosshair.setCrosshairPosition(lockSystem.currentEnemy().GetComponent<Enemy>().lookAtPoint.transform);
                    center.position = centerPosition;
                    vcamera.m_LookAt = center;
                } else lockSystem.isLocked = false;
            }
            else
            {
                vcamera.m_LookAt = transform; 
                lockSystem.isLocked = false;
            }
        }
        if (Input.GetButtonDown(toggleLockName) && (lockSystem.isLocked == true || lockSystem.currentEnemy() == null))
        {
            lockSystem.toggleEnemy();
            crosshair.setCrosshairPosition(lockSystem.currentEnemy().GetComponent<Enemy>().lookAtPoint.transform);
        }

        if(lockSystem.isLocked == true && checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) >= 2f)
        {
            Vector3 centerPosition = (lockSystem.currentEnemy().transform.position + transform.position) / 2;
            center.position = centerPosition;
            vcamera.m_LookAt = center;
        }

        if(lockSystem.isLocked == true && (checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) <= 2f ||
            checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) >= 25f))
        {
            vcamera.m_LookAt = transform;
            lockSystem.isLocked = false;
        }
    }

    float checkMagnitude(Vector3 value1, Vector3 value2)
    {
        return Vector3.Distance(value1, value2);
    }
}
