using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{

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
  }

  // Update is called once per frame
  void FixedUpdate()
  {

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




