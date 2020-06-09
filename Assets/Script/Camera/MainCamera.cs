using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
  [SerializeField] private Transform target;

  [SerializeField] private Vector3 offset;
  [SerializeField] private Vector3 offsetAir;
  
  [SerializeField] private float smooth;


  void Start()
  {
  }

  void LateUpdate()
  {
    if (target.GetComponent<PlayerMovement>().IsInAir())
    {
      Debug.Log("air");
      transform.position = Vector3.Lerp(transform.position, target.position + offsetAir, smooth * Time.deltaTime);
    }
    else
    {
      Debug.Log("ground");

      transform.position = Vector3.Lerp(transform.position, target.position + offset, smooth * Time.deltaTime);
    }
  }
}
