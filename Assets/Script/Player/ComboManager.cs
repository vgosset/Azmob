using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private float d_combo;
    
    public Animator anim;

    public float fireRate;
    public float comboRate;

    [HideInInspector]
    public int a_combo_hit;
    [HideInInspector]
    public int a_combo_type;

    [HideInInspector]
    public float timer_hit;
    [HideInInspector]
    public float fireTimer;
    [HideInInspector]
    public float timer_type;

    public void PlusOne()
    {
        int id = 0;

        a_combo_hit++;
        timer_hit = d_combo;
        
        if (a_combo_hit % 3 == 0)
          id = 1;

        if (a_combo_hit == 1)
        {
          UiManager.Instance.ComboState(2);
          StartCoroutine(DelayedComboUpdate(0.15f, id));
        }
        else
        {
          UiManager.Instance.PlusOne(a_combo_hit, id);
        }
    }
    
    private IEnumerator DelayedComboUpdate(float d, int id)
    {
      yield return new WaitForSeconds(d);
      UiManager.Instance.PlusOne(a_combo_hit, id);
    }
    public void DefineAttackType()
    {
        fireTimer = fireRate;

        if (a_combo_type <= 3)
        {
        }
        else
        {
          a_combo_type = 0;
        }

        if (a_combo_type == 3)
          fireTimer = comboRate;

        anim.SetTrigger("attack" + (a_combo_type + 1).ToString());

        a_combo_type++;
        timer_type = 1;
    }
}
