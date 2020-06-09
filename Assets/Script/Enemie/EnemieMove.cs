using System.Collections;
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

    public float t_hit;
    public float t_go;

    public enum M_State
    {
        GO_TO,
        HIT,
    }
     
    public IEnumerator SetRunLoop()
     {
         while(true)
         {
             yield return new WaitForSeconds(t_hit);
             m_state = M_State.HIT;
             yield return new WaitForSeconds(t_go);
             m_state = M_State.GO_TO;
         }
     }

    public void Move(float  dist)
    {
        if (m_state == M_State.HIT)
        {
            // MoveAround();
            MoveTo();
        }
        else if (m_state == M_State.GO_TO)
        {
            Follow();
        }
    }

    private void Follow()
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
    }
}
