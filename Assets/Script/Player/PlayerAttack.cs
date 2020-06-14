using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : ComboManager
{
    private PlayerMovement p_movement;
    private Weapon p_weapon;


    [SerializeField] private float delayInAir;


    private bool a_isJump;

    private void Awake()
    {
        p_movement = GetComponent<PlayerMovement>();
        p_weapon = transform.GetChild(0).GetComponent<Weapon>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        float dt = Time.deltaTime;

        TimerHandler(dt);
    }
    private void TimerHandler(float dt)
    {
        if (fireTimer > 0)
        {
            fireTimer -= dt;
        }
        if (timer_hit < 0 && a_combo_hit > 0)
        {
          a_combo_hit = 0;
          UiManager.Instance.ComboState(3);
        }
        else if (timer_hit > 0)
        {
          timer_hit -= dt;
        }
        if (timer_type < 0)
        {
          a_combo_type = 0;
        }
        else if (timer_type > 0)
        {
          timer_type -= dt;
        }
    }
    public void Fire()
    {
        if (fireTimer <= 0)
        {
            FireDetection();
        }
    }
    
    private void Attack()
    {
        DefineAttackType();

        StopAllCoroutines();
        StartCoroutine(FreezMovement());
    }
    private void JumpAttack()
    {
        fireTimer = fireRate * 1.2f;
        anim.SetTrigger("jumpAttack");
        p_movement.BlockJump();
    }
    public void PlayerJump()
    {
        p_movement.Jump();
    }
    private IEnumerator FreezMovement()
    {
      p_movement.FreezeJump();

      yield return new WaitForSeconds(delayInAir);

      p_movement.UnFreezeJump();
    }
    private void ResetAllHit()
    {
        var e_life = FindObjectsOfType<EnemieLife>();

        foreach (EnemieLife enemy in e_life)
        {
            enemy.ResetHit();
        }
    }

    //////////////////////////////////////// Tools //////////////////////////////////////////////
    
    public void FireActive()
    {
        p_weapon.SetState(true);
    }
    public void FireEnd()
    {
        p_weapon.SetState(false);
        
        ResetAllHit();
    }

    public void SetJumpAttack(int id)
    {
        switch (id)
        {
            case 0:
                    Debug.Log("jump true");
                a_isJump = true;
            break;
            default:
                    Debug.Log("jump false");
                a_isJump = false;
            break;
        }
    }
    private void FireDetection()
    {
        if (p_movement.IsPend())
        {
            JumpAttack();
        }
        else
        {
            Attack();
        }
    }
    public void HasHit()
    {
        PlusOne();
    }
}
