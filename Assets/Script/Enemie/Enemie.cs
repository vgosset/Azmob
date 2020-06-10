﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : EnemieMove
{
    [SerializeField] private ParticleSystem die;
    [SerializeField] private ParticleSystem blood;
    
    [SerializeField] private float hitUpHeigth;
    [SerializeField] private float hitUpSpeed;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float delayInAir;

    private Animator anim;
    private State state;

    private bool getHitUp = false;

    enum State
    {
      FALL,
      HITUP,
      FREEZ,
      MOVE,
      NONE
    }

    private void Awake()
    {
        state = State.MOVE;
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
        if (state == State.HITUP)
        {
            Vector3 dest = new Vector3(transform.position.x, hitUpHeigth, transform.position.z);
            if (GoToDest(dest, hitUpSpeed))
            {
            }
            else
                state = State.FALL;
        }
        else if (state == State.MOVE)
        {
            Move(distance);
        }
        else if (state == State.FALL)
        {
            Vector3 fallingDest = new Vector3(transform.position.x, -4.3f, transform.position.z);

            if (GoToDest(fallingDest, fallSpeed))
            {
            }
            else
                state = State.MOVE;
        }
    }
    public void Freeze()
    {
        StartCoroutine(FreezMovement());
    }
    private IEnumerator FreezMovement()
    {
        FreezeJump();

        yield return new WaitForSeconds(delayInAir);

        UnFreezeJump();
    }
    public void FreezeJump()
    {
        state = State.FREEZ;
    }
    public void UnFreezeJump()
    {
        state = State.FALL;
    }
    public void HitUp()
    {
        state = State.HITUP;
    }
    private bool GoToDest(Vector3 dest, float multi)
    {
      if(Vector3.Distance(dest, transform.position) >= 0.01f)
      {
        float step = Time.deltaTime * multi;

        transform.position = Vector3.MoveTowards(transform.position, dest, step);
        return true;
      }
      return false;
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
