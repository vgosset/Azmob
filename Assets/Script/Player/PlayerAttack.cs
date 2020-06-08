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
            fireTimer = fireRate;
            anim.SetTrigger("attack");

            StopAllCoroutines();
            StartCoroutine(FreezMovement());
        }
    }

    public void FireActive()
    {
        p_weapon.SetState(true);
    }
    public void FireEnd()
    {
        p_weapon.SetState(false);
    }
    public void HasHit()
    {
        PlusOne();
    }

    private IEnumerator FreezMovement()
    {
      p_movement.FreezeJump();

      yield return new WaitForSeconds(delayInAir);

      p_movement.UnFreezeJump();
    }
}
