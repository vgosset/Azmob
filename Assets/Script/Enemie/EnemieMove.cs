﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieMove : MonoBehaviour
{

    public M_State m_state;
    public Transform Target;

    public float distCac;
    public float distance;
    public float followSpeed;
    public float runMulti;
    public float fireDelay;

    public Animator anim;
    
    public Vector2 t_hit;
    public Vector2 t_go;

    public Material mat_hit;
    public Material mat_go;

    public float fireTimer;

    public MeshRenderer mesh;

    public enum M_State
    {
        GO_TO,
        HIT,
    }
     
    public IEnumerator SetRunLoop()
     {
         while(true)
         {
             float R_t_hit = Random.Range(t_hit.x, t_hit.y);
             float R_t_go = Random.Range(t_go.x, t_go.y);

             yield return new WaitForSeconds(R_t_hit);

             m_state = M_State.HIT;
             mesh.material = mat_hit;
             
             yield return new WaitForSeconds(R_t_go);
             
             m_state = M_State.GO_TO;
             mesh.material = mat_go;
         }
     }

    public void Move(float  dist)
    {
        if (m_state == M_State.HIT)
        {
            MoveTo();
        }
        else if (m_state == M_State.GO_TO)
        {
            GoTo();
        }
    }

    private void GoTo()
    {
        float step = Time.deltaTime * followSpeed;

        Vector3 dest = new Vector3(Target.position.x + distance, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, dest, step);
    }
    public void MoveTo()
    {
        float step = Time.deltaTime * followSpeed * runMulti;

        Vector3 dest = new Vector3(Target.position.x + distCac, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, dest, step);

        if(Vector3.Distance(dest, transform.position) <= 0.7f)
        {
            if (fireTimer <= 0)
            {
                fireTimer = fireDelay;
                Fire();
            }
        }
    }
    private void Fire()
    {
        anim.SetTrigger("attack");
    }
}
