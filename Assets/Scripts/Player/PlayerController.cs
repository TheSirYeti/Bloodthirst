using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Transform center;
    public AutoAimAI aimAI;
    public SpecialBar specialSlider;

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
        EventManager.resetEventDictionary();

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
            if (movement.groundCheck.getStatus() > 0 && !movement.isAttacking)
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
                EventManager.Trigger("AutoAim");
                switch (basicAttacks.currentWeapon)
                {
                    case 0:
                        basicAttacks.attack();
                        break;
                    case 1:
                        basicAttacks.heavyAttack();
                        break;
                }
            }
        }

        if (Input.GetButtonDown("Fire4"))
        {
            basicAttacks.checkCombo();
        }

        if (Input.GetButtonDown("HeavyAttack") && specialSlider.GetSpecialValue() == 1f && !movement.isAttacking)
        {
            basicAttacks.specialAttack();
            specialSlider.ResetSpecialValue(null);
        }


        if (specialSlider.GetSpecialValue() >= 1f)
        {
            EventManager.Trigger("EnableSpecialParticles");
        } else EventManager.Trigger("DisableSpecialParticles");


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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void SlowAttackingSpeed()
    {
        movement.slowMovement();
        movement.isAttacking = true;
    }

    public void NormalAttackingSpeed()
    {
        movement.resetMovement();
        movement.isAttacking = false;
    }
}
