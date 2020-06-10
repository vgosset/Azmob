using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool state;

    private PlayerAttack p_attack;

    bool jumpAttack;

    private void Awake()
    {
        p_attack = transform.parent.GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        
    }
    public void SetState(bool s)
    {
        state = s;
    }
    public void SetAttackType(int id)
    {
        switch (id)
        {
            case 0:
                jumpAttack = true;
            break;
            default:
                jumpAttack = false;
            break;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (state)
        {
            if (col.tag == "enemie")
            {
                int id;

                if (jumpAttack)
                {
                    id = 1;
                }
                else
                {
                    id = 0;
                }

                if (col.transform.GetComponent<EnemieLife>().GetHit(id, 1))
                {
                    p_attack.HasHit();
                }
            }
        }
    }
}
