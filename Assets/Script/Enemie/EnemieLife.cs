using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieLife : MonoBehaviour
{
    [SerializeField] private float a_life;

    [SerializeField] private ParticleSystem die;
    [SerializeField] private ParticleSystem blood;

    private Enemie m_enemie;
    
    private Animator anim;
    
    private bool alive = true;
    private bool hasHit = false;

    private void Awake()
    {
        m_enemie = GetComponent<Enemie>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
    }

    void Update()
    {
        
    }

    public bool GetHit(int id, int value)
    {
        if (alive && !hasHit)
        {
            a_life -= value;
            
            if (a_life <= 0)
            {
                UiManager.Instance.HasKill();
                Die();
            }
            else
                anim.SetTrigger("bled");

            switch (id)
            {
                case 0:
                    m_enemie.Freeze();
                break;
                default:
                    m_enemie.HitUp();
                break;
            }
            hasHit = true;
            return true;
        }
        return false;
    }
    private void Die()
    {
        alive = false;

        m_enemie.enabled = false;
        
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
