using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private float d_combo;
    
    private int a_combo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlusOne()
    {
        int id = 0;

        a_combo++;
        
        if (a_combo % 3 == 0)
          id = 1;

        if (a_combo == 1)
        {
          UiManager.Instance.ComboState(2);
          StartCoroutine(DelayedComboUpdate(0.15f, id));
        }
        else
        {
          UiManager.Instance.PlusOne(a_combo, id);
        }
        StartCoroutine(ResetCombo());
    }
    
    private IEnumerator DelayedComboUpdate(float d, int id)
    {
      yield return new WaitForSeconds(d);
      UiManager.Instance.PlusOne(a_combo, id);
    }
    private IEnumerator ResetCombo()
    {
      yield return new WaitForSeconds(d_combo);
      a_combo = 0;
      
      UiManager.Instance.ComboState(3);
    }
}
