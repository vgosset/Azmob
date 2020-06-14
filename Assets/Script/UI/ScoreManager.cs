using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text display;

    [SerializeField] private float a_base;
    [SerializeField] private float a_multi;

    [SerializeField] private float a_kill_base;

    private float c_amount;

    void Start()
    {
        c_amount = 0;
        UpdateScore();
    }

    void Update()
    {
        
    }
    public void UpdateOnCombo(int amount)
    {
        float multi = 1 + (a_multi * (amount - 1));
        float f_amount = a_base * multi;
        
        c_amount += f_amount;

        UpdateScore();
    }
    public void UpdateOnKill()
    {
        c_amount += a_kill_base;
        UpdateScore();
    }
    private void UpdateScore()
    {
        display.text = c_amount.ToString();
    }
}
