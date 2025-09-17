using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{

  public float baseSpeed = 3f;  //starting roll speed
  public float pushForce = 6f;  //strength per click
  public float maxSpeed = 10f;  // hard speed cap


  public SideTrigger feet;
  public SideTrigger head;
  public SideTrigger nose;
  public SideTrigger tail;

  private Rigidbody2D rb;

  private bool canPush = false;
  private float lastPushTime = 0f;
  private float pushCooldown = 0.5f;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    CheckPushInput();
    CheckTouchingSides();

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
      TryPushCooldown();
    }
  }

  void TryPushCooldown()
  {
    if (Time.time - lastPushTime >= pushCooldown)
    {
      canPush = true;
      lastPushTime = Time.time;
    }
  }

  void ApplyPushForce()
  {
    //Debug.Log("Push!");
    rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
    canPush = false;
  }

  void CheckTouchingSides()
  {
    if (feet.isTouchingGround)
    {
      Debug.Log("Feet");
    }

    if (head.isTouchingGround)
    {
      Debug.Log("Head");
    }

    if (nose.isTouchingGround)
    {
      Debug.Log("Nose");
    }

    if (tail.isTouchingGround)
    {
      Debug.Log("Tail");
    }
  }

}




