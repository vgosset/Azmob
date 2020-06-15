using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : ComboManager
{
    private PlayerMovement p_movement;
    private Weapon p_weapon;


    [SerializeField] private float delayInAir;
    [SerializeField] private float jumpDelay;

    private float j_attackTimer;
    private bool j_attackOn = false;
    private bool a_typeJump = false;
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
        
        if (j_attackOn)
        {
            j_attackTimer += dt;
            if (j_attackTimer > jumpDelay)
                a_typeJump = true;
        }
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
            Invoke("FireDetection", 0.05f);
        }
    }
    public void StateTimerJump(bool state)
    {
        j_attackOn = state;

        if (!state)
            j_attackTimer = 0;
    }
    private void Attack()
    {
        DefineAttackType();

        StopAllCoroutines();
        StartCoroutine(FreezMovement());
    }
    private void JumpAttack()
    {
        p_weapon.SetAttackType(0);

        fireTimer = fireRate * 1.5f;
        anim.SetTrigger("jumpAttack");
        p_movement.BlockJump();
        a_typeJump = false;
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
    public void UpdateJumpAttackTimer()
    {
        j_attackTimer += Time.deltaTime;
        Debug.Log(j_attackTimer);
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
                a_isJump = true;
            break;
            default:
                a_isJump = false;
            break;
        }
    }
    private void FireDetection()
    {
        if (/*p_movement.IsPend() ||*/ a_typeJump)
        {
            JumpAttack();
        }
        else if (p_movement.A_down)
        {
            if (p_movement.IsFall())
                DownAirAttack();
            else if (p_movement.IsOnGround())
                DownAttack();
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
    private void DownAirAttack()
    {
        fireTimer = fireRate * 1.5f;

        anim.SetTrigger("downAir");
        // p_movement.DashDownAir();
    }
    private void DownAttack()
    {
        fireTimer = fireRate * 3f;

        anim.SetTrigger("downAttack");
    }
}
