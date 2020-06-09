using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : EnemieMove
{
    [SerializeField] private ParticleSystem die;
    [SerializeField] private ParticleSystem blood;

    private Animator anim;

    private void Awake()
    {
        m_state = M_State.GO_TO;

        anim = GetComponent<Animator>();

        StartCoroutine("SetRunLoop");
    }

    private void Start()
    {
        Target = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        PosUpdate();
    }
    
    private void LateUpdate()
    {
        Move(distance);
    }
    public void BloodEffect()
    {
        blood.Play();
    }
    public void DieEffect()
    {
        die.Play();
    }
    public void Dead()
    {
        Invoke("DestroySelf", 2);
    }
    private void DestroySelf()
    {
      Destroy(this.gameObject);
    }
    public void PosUpdate()
    {
        if (transform.position.x > Target.position.x)
        {
            distance = Mathf.Abs(distance);
            distCac = Mathf.Abs(distCac);

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            distance = -Mathf.Abs(distance);
            distCac = -Mathf.Abs(distCac);

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
