using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : MonoBehaviour
{
    [SerializeField] private Text amount;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateTxt(int amout)
    {
        amount.text = amout.ToString();
    }
    public void TriggerAnim(int id)
    {
        switch (id)
        {
            case 0:
                anim.SetTrigger("normal");
            break;
            case 1:
                anim.SetTrigger("hard");
            break;
            case 2:
                anim.SetTrigger("appear");
            break;
            case 3:
                anim.SetTrigger("reset");
            break;
        }
    }
    public void SetPos(int id)
    {
        switch (id)
        {
            case 0:
                transform.localPosition = new Vector2(700, 0);
            break;
            case 1:
                transform.localPosition = new Vector2(-700, 0);
            break;
        }
    }
}
