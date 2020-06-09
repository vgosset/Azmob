using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieLife : MonoBehaviour
{
    [SerializeField] private float a_life;
    private Enemie m_enemie;
    
    private Animator anim;
    
    private bool alive = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void GetHit(int value)
    {
        if (alive)
        {
            a_life -= value;
            
            if (a_life <= 0)
            {
                Die();
                Debug.Log("dead");
            }
            else
                anim.SetTrigger("bled");
        }
    }
    private void Die()
    {
        alive = false;
        anim.SetTrigger("die");
    }
}
