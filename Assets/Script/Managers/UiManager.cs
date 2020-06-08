using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] private ComboDisplay d_combo;


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
    public void PlusOne(int amount, int id)
    {
        d_combo.TriggerAnim(id);
        d_combo.UpdateTxt(amount);
    }
    public void SetComboPos(int id)
    {
        d_combo.SetPos(id);
    }
    public void ComboState(int id)
    {
        d_combo.TriggerAnim(id);
    }
}
