using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private VariableJoystick joysticMov;
    
    public float jumpSpeed;
    public float jumpHeight;
    public float fallingSpeed;
    public float jumpDelay;

    private Vector3 direction;
    private Vector3 jumpDest;
    private Vector3 fallingDest;

    private bool jump = false;
    private bool jumpPending = false;
    private bool falling = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (jump)
        {
          JumpBehavior();
        }
        if (!jump || falling)
        {
          GetDir();
          SetRot();
          Move();
          JumpDetection();
        }
        
    }
    private void JumpBehavior()
    {
      if (falling)
      {
        fallingDest = new Vector3(transform.position.x, -4.3f, transform.position.z);

        if (GoToDest(fallingDest, fallingSpeed))
        {
          
        }
        else
        {
          falling = false;
          jump = false;
          jumpPending = false;
        }
      }
      else
      {
        if (GoToDest(jumpDest, jumpSpeed))
        {
          
        }
        else
          falling = true;
      }
    }
    private bool GoToDest(Vector3 dest, float speed)
    {
      if(Vector3.Distance(dest, transform.position) >= 0.01f)
      {
        float step = Time.deltaTime * speed;

        transform.position = Vector3.MoveTowards(transform.position, dest, step);
        return true;
      }
      return false;
    }
    private void JumpDetection()
    {
      if (!jumpPending && joysticMov.Vertical >= 0.8f)
        StartCoroutine(DelayedJump());
    }
    private IEnumerator DelayedJump()
    {
      jumpPending = true;

      yield return new WaitForSeconds(jumpDelay);
      
      Jump();
    }
    public void Jump()
    {
      Debug.Log("JUMP");

      Vector3 dest = transform.position + new Vector3(0, jumpHeight, 0);

      jumpDest = dest;
      jump = true;
    }

    private void GetDir()
    {
      int x = 0;

      if (joysticMov.Horizontal < 0)
        x = -1;
      else if (joysticMov.Horizontal > 0)
        x = 1;
      direction = new Vector3(x, 0, 0);
    }

    private void Move()
    {
      if (direction.x != 0)
      {
        SetRot();

        Vector3 dest = new Vector3(direction.x, 0, 0);
        
        transform.position += dest / 4;
      }
    }
    private void SetRot()
    {
      if (direction.x > 0)
        transform.rotation = Quaternion.Euler(0, 0, 0);
      else if (direction.x < 0)
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
