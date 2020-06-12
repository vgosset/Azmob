using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieWeapon : MonoBehaviour
{
    private bool state;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetState(bool s)
    {
        state = s;
    }
    void OnTriggerStay(Collider col)
    {
        if (state)
        {
            if (col.tag == "Player")
            {
                if (col.transform.GetComponent<PlayerLife>().GetHit(1))
                {
                    // p_attack.HasHit();
                }
            }
        }
    }
}
