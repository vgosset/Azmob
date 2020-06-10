using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : ComboManager
{
    private PlayerMovement p_movement;
    private Weapon p_weapon;

    private Animator anim;

    [SerializeField] private float delayInAir;
    [SerializeField] private float fireRate;

    private float fireTimer;

    private bool a_isJump;

    private void Awake()
    {
        p_movement = GetComponent<PlayerMovement>();
        p_weapon = transform.GetChild(0).GetComponent<Weapon>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
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
        fireTimer = fireRate;
        anim.SetTrigger("attack");

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
