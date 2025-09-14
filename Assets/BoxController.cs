using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{


  public Color baseColor = Color.white;
  public Color chargedColor = Color.yellow;
  private SpriteRenderer sr;

  private Rigidbody2D rb;

  public bool canJump; // sides
  
  private float dotProductBox;

  public SideTrigger bottomSide;
  public SideTrigger leftSide;
  public SideTrigger rightSide;
  public SideTrigger topSide;

  public ParticleSystem chargeParticles;


  


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

  bool isGrounded()
  {
    return bottomSide.isTouchingGround || leftSide.isTouchingGround || rightSide.isTouchingGround
      || topSide.isTouchingGround;
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




