using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool state;
    private bool hasHit;

    private PlayerAttack p_attack;

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
        hasHit = false;
    }
    void OnTriggerStay(Collider col)
    {
        if (state && !hasHit)
        {
            if (col.tag == "enemie")
            {
                p_attack.HasHit();
                hasHit = true;
            }
        }
    }
}
