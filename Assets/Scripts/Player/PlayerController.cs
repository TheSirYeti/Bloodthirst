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
    public AutoAimAI aimAI;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButtonName = "Jump";
    public string attackButtonName = "Fire1";
    public string lockButtonName = "LockButton";
    public string toggleLockName = "ToggleLock";
    public string switchWeaponName = "SwitchWeapon";

    public SpecialAttackBar bar;
    public GameObject deathPanel, deathButton;

    private void Awake()
    {
        if(CheckpointBehaviour.instance != null)
            transform.position = CheckpointBehaviour.instance.GetCurrentSpawnpoint().position;
    }

    private void Start()
    {
        movement.enabled = true;
    }
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
                aimAI.lookAtEnemy();
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

        if (bar.getValue() == 1f && hpManager.hp > 0)
        {
            playerVFX.enableSparks();
        } else playerVFX.disableSparks();


        if (Input.GetButtonDown("SwitchWeapon") && !movement.isAttacking)
        {
            basicAttacks.changeWeapons();
        }

        if (hpManager.amIHurt)
        {
            if(!movement.isAttacking && !basicAttacks.isInvunerable)
            {
                hpManager.amIHurt = false;
                takeDamage();
            }
        }

        if (Input.GetButtonDown("Roll"))
        {
            movement.roll();
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
