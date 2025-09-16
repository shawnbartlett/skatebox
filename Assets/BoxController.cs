using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{

  public float baseSpeed = 3f;  //starting roll speed
  public float pushForce = 6f;  //strength per click
  public float maxSpeed = 10f;  // hard speed cap


  public SideTrigger feetTrigger;



  private Rigidbody2D rb;

  public SideTrigger bottomSide;
  public SideTrigger leftSide;
  public SideTrigger rightSide;
  public SideTrigger topSide;

  private bool canPush = false; 

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    CheckPushInput();
    CheckGroundedSide();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (canPush)
    {
      ApplyPushForce();
    }
  }

  void CheckPushInput()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Debug.Log("Mouse Clicked!");
      canPush = true;
    }
  }

  void ApplyPushForce()
  {
    Debug.Log("Push!");
    rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
    canPush = false;
  }


  void CheckGroundedSide()
  {
    if (bottomSide.isTouchingGround)
    {
      //Debug.Log(bottomSide.sideType);
    }
    if (leftSide.isTouchingGround)
    {
      //Debug.Log(leftSide.sideType);
    }
    if (rightSide.isTouchingGround)
    {
      //Debug.Log(rightSide.sideType);
    }
    if (topSide.isTouchingGround)
    {
      //Debug.Log(topSide.sideType);
    }
  }
}




