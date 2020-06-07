using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public float fireRate;
    private float fireTimer;

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
        }
    }
    public void FireHit()
    {

    }
}
