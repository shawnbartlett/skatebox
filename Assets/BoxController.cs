using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{

  public float baseSpeed = 3f;  //starting roll speed
  public float pushImpulse = 6f;  //strength per click
  public float maxSpeed = 10f;  // hard speed cap
  public float clickCooldown = 0.18f; // time between clicks
  float lastClickTime = -999f;
  bool isRunning = false;
  bool canKick = false;
  public SideTrigger feetTrigger;



  private Rigidbody2D rb;

  public SideTrigger bottomSide;
  public SideTrigger leftSide;
  public SideTrigger rightSide;
  public SideTrigger topSide;


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    
  
  }

  void Update()
  {
    CheckGroundedSide();

    if (!isRunning && Input.GetMouseButtonDown(0))
    {
      isRunning = true;
      canKick = true;
      lastClickTime = Time.time;
    }

    else if (isRunning && Input.GetMouseButtonDown(0))
    {
      if (feetTrigger.isTouchingGround
      && (Time.time - lastClickTime) >= clickCooldown
      && rb.velocity.x < maxSpeed)
      {
        canKick = true;
        lastClickTime = Time.time;
      }
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (canKick)
    {
      rb.AddForce(Vector2.right * pushImpulse, ForceMode2D.Impulse);
      canKick = false;
    }

    if (isRunning && rb.velocity.x > maxSpeed)
    {
      rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
    }
  }

  void CheckGroundedSide()
  {
    if (bottomSide.isTouchingGround)
    {
      Debug.Log(bottomSide.sideType);
    }
    if (leftSide.isTouchingGround)
    {
      Debug.Log(leftSide.sideType);
    }
    if (rightSide.isTouchingGround)
    {
      Debug.Log(rightSide.sideType);
    }
    if (topSide.isTouchingGround)
    {
      Debug.Log(topSide.sideType);
    }
  }

}




