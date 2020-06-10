using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAttack p_attack;
    private Weapon weapon;

    [SerializeField] private VariableJoystick joysticMov;
    
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDelay;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float fallingSpeed;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float airSpeed;

    [SerializeField] private float j_multi;
    [SerializeField] private float f_multi;

    private State state;

    private Vector3 direction;
    private Vector3 jumpDest;
    private Vector3 fallingDest;

    private float speed;
    private bool jumpFreeze;

    enum State
    {
      FALL,
      JUMP,
      J_PEND,
      FREEZ,
      NONE
    }
    private void Awake()
    {
        p_attack = GetComponent<PlayerAttack>();
        weapon = transform.GetChild(0).GetComponent<Weapon>();
    }

    void Start()
    {
        state = State.NONE;
    }

    void Update()
    {
        if (state == State.FREEZ)
        {
          
        }
        else 
        {
          Movement();
        }
    }
    private void Movement()
    {
      if (state == State.FALL || state == State.JUMP)
      {
        JumpBehavior();
      }
      if (state == State.FALL || state == State.NONE)
      {
        GetDir();
        SetRot();
        Move();
        JumpDetection();
      }
    }
    private void Move()
    {
      if (direction.x != 0)
      {
        SetRot();

        Vector3 dest = new Vector3(direction.x, 0, 0);

        if (IsFall())
          transform.position += dest * airSpeed;
        else
          transform.position += dest * groundSpeed;
      }
    }
    private void JumpBehavior()
    {
      if (state == State.FALL)
      {
        fallingDest = new Vector3(transform.position.x, -4.3f, transform.position.z);

        if (GoToDest(fallingDest, f_multi))
        {
        }
        else
          state = State.NONE;
      }
      else
      {
        if (GoToDest(jumpDest, j_multi))
        {
        }
        else
        {
          speed = fallingSpeed;
          state = State.FALL;
        }
      }
    }
    
    private void JumpDetection()
    {
      if (state == State.NONE && joysticMov.Vertical >= 0.8f)
        StartCoroutine(DelayedJump());
    }
    private IEnumerator DelayedJump()
    {
      state = State.J_PEND;

      weapon.SetAttackType(0);
      yield return new WaitForSeconds(jumpDelay);
      
      if (!jumpFreeze)
        Jump();
    }
    public void BlockJump()
    {
      jumpFreeze = true;
    }
    public void Jump()
    {
      weapon.SetAttackType(1);

      if (state != State.JUMP)
      {
        Vector3 dest = transform.position + new Vector3(0, jumpHeight, 0);

        jumpDest = dest;
        speed = jumpSpeed;
        state = State.JUMP;
        jumpFreeze = false;
      }
    }
    
    /////////////////////////////////////// Tools ///////////////////////////////////////////////////

    public void FreezeJump()
    {
      if (state != State.NONE)
      {
        state = State.FREEZ;
      }
    }
    public void UnFreezeJump()
    {
        state = State.FALL;
    }
    private bool GoToDest(Vector3 dest, float multi)
    {
      if(Vector3.Distance(dest, transform.position) >= 0.01f)
      {
        float step = Time.deltaTime * speed;

        speed += Time.deltaTime * multi;

        transform.position = Vector3.MoveTowards(transform.position, dest, step);
        return true;
      }
      return false;
    }

    private void SetRot()
    {
      if (direction.x > 0)
      {
        UiManager.Instance.SetComboPos(0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
      }
      else if (direction.x < 0)
      {
        UiManager.Instance.SetComboPos(1);
        transform.rotation = Quaternion.Euler(0, 180, 0);
      }
    }
    public bool IsFall()
    {
      if (state == State.FALL)
        return true;
      return false;
    }
    public bool IsPend()
    {
      if (state == State.J_PEND)
        return true;
      return false;
    }
    public bool IsInAir()
    {
      if (state == State.FALL || state == State.JUMP || state == State.FREEZ)
        return true;
      return false;
    }

    private void GetDir()
    {
      float x = 0;

      if (joysticMov.Horizontal <= -0.3f)
        x = -0.1f;
      else if (joysticMov.Horizontal >= 0.3f)
        x = 0.1f;
      direction = new Vector3(x, 0, 0);
    }
}
