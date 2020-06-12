using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int a_life;

    [SerializeField] private ParticleSystem die;
    [SerializeField] private ParticleSystem blood;

    private Animator anim;

    private bool alive = true;
    private bool hasHit = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public bool GetHit(int value)
    {
        if (alive && !hasHit)
        {
            a_life -= value;
            
            if (a_life <= 0)
            {
                Die();
            }
            else
                anim.SetTrigger("bled");

            hasHit = true;
            return true;
        }
        return false;
    }
    private void Die()
    {
        alive = false;
        anim.SetTrigger("die");
    }
    public void ResetHit()
    {
        hasHit = false;
    }
    public void BloodEffect()
    {
        blood.Play();
    }
    public void DieEffect()
    {
        die.Play();
    }
}
