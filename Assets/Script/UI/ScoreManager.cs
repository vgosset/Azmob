using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text d_combo;
    [SerializeField] private Text d_final;

    [SerializeField] private float a_base;
    [SerializeField] private float a_multi;

    [SerializeField] private float a_kill_base;

    private float c_amount;
    private float t_amount;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        c_amount = 0;
        anim.SetTrigger("on");
    }

    private void Update()
    {
        
    }

    public void UpdateOnCombo(int amount)
    {
        float multi = 1 + (a_multi * (amount - 1));
        float f_amount = a_base * multi;
        
        c_amount += f_amount;

        anim.SetTrigger("addCombo");

        UpdateComboScore();
    }

    public void UpdateOnKill()
    {
        c_amount += a_kill_base;
        UpdateComboScore();
    }

    public void UpdateComboScore()
    {
        d_combo.text = c_amount.ToString();
    }
    public void ComboEnd()
    {
        t_amount += c_amount;
        c_amount = 0;

        anim.SetTrigger("addTotal");

    }
    public void UpdateFinalScore()
    {
        d_final.text = t_amount.ToString();
    }
}
