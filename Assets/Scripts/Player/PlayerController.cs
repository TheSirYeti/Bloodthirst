using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Behaviour;
using Player.Animations;
using VFX.Player;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Movement movement;
    public BasicAttacks basicAttacks;
    public swordEffects swordEffects;
    public PlayerLife hpManager;
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
    public string switchWeaponName = "SwitchWeapon";

    public SpecialAttackBar bar;
    public GameObject deathPanel, deathButton;
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
            }
        }
        if (Input.GetButtonDown(attackButtonName) && basicAttacks.checkAttackCooldown())
        {
            if (movement.groundCheck.getStatus() > 0) {
                //bar.addValue(0.05f);
                switch (basicAttacks.currentWeapon)
                {
                    case 0:
                        basicAttacks.attack();
                        movement.restrictMovement(0.55f);
                        break;
                    case 1:
                        basicAttacks.heavyAttack();
                        movement.restrictMovement(1f);
                        break;
                }
            }
        }

        if (Input.GetButtonDown("Fire4"))
        {
            basicAttacks.checkCombo();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            bar.addValue(1);
        }

        if (Input.GetButtonDown("HeavyAttack") && bar.getValue() == 1f && !movement.isAttacking)
        {
            basicAttacks.specialAttack();
            bar.resetValue();
        }

        if (bar.getValue() == 1f)
        {
            playerVFX.enableSparks();
        } else playerVFX.disableSparks();


        if (Input.GetButtonDown("SwitchWeapon") && !movement.isAttacking)
        {
            basicAttacks.changeWeapons();
        }
        basicAttacks.resetAttackTurn();
        lockingSystem();

        if (hpManager.amIHurt)
        {
            if(!movement.isAttacking && !basicAttacks.isInvunerable)
            {
                hpManager.amIHurt = false;
                takeDamage();
            }
        }
    }

    private void FixedUpdate()
    {
        movement.Move();
    }

    float checkMagnitude(Vector3 value1, Vector3 value2)
    {
        return Vector3.Distance(value1, value2);
    }

    void lockingSystem()
    {
        float toggle = Input.GetAxis("Camera X");

        if (Input.GetButtonDown(lockButtonName))
        {
            if (lockSystem.isLocked == false)
            {
                lockSystem.isLocked = true;
                lockSystem.index = 0;
                if (lockSystem.checkEnemiesAround() && checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) >= 2f && lockSystem.currentEnemy() != null)
                {
                    Vector3 centerPosition = (lockSystem.currentEnemy().GetComponent<Enemy>().lookAtPoint.transform.position + transform.position) / 2;
                    crosshair.setCrosshairPosition(lockSystem.currentEnemy().GetComponent<Enemy>().lookAtPoint.transform);
                    crosshair.enableImage();
                    center.position = centerPosition;
                    vcamera.m_LookAt = center;
                    vcamera.m_XAxis.m_InputAxisName = "No Movement";
                }
                else lockSystem.isLocked = false;
            }
            else
            {
                vcamera.m_XAxis.m_InputAxisName = "Camera X";
                vcamera.m_LookAt = transform;
                lockSystem.isLocked = false;
                crosshair.disableImage();
            }
        }
        if (lockSystem.isLocked == true)
        {
            lockSystem.toggleEnemy(toggle);
            if(lockSystem.currentEnemy() != null)
                crosshair.setCrosshairPosition(lockSystem.currentEnemy().GetComponent<Enemy>().lookAtPoint.transform);
        }
        if(lockSystem.currentEnemy() != null)
        {
            if (lockSystem.isLocked == true && checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) >= 2f)
            {
                Vector3 centerPosition = (lockSystem.currentEnemy().transform.position + transform.position) / 2;
                center.position = centerPosition;
                vcamera.m_LookAt = center;
            }
        }

        if(lockSystem.currentEnemy() != null)
        {
            if (lockSystem.isLocked == true && (checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) <= 2f ||
                                                checkMagnitude(transform.position, lockSystem.currentEnemy().transform.position) >= 25f))
            {
                vcamera.m_LookAt = transform;
                lockSystem.isLocked = false;
                vcamera.m_XAxis.m_InputAxisName = "Camera X";
                crosshair.disableImage();
            }
        }
    }

    void takeDamage()
    {

    }


    public void showDeathPanel()
    {
        deathPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(deathButton);
        SoundManager.instance.StopAllSounds();
        Time.timeScale = 0f;
    }


}
