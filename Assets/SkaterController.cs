using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SkaterController : MonoBehaviour
{

  public GroundTrigger groundCheck;

  private Rigidbody2D rb;
  public SpriteRenderer spriteRenderer;
  public Sprite rideSprite;
  public Sprite pushSprite;
  public Sprite crouchSprite;
  public Sprite jumpSprite;
  public Sprite crouchRight;
  public Sprite crouchLeft;

  public float jumpTimeThreshold = 0.2f;
  private float jumpTime;
  public float jumpForce = 5f;
  public float pushForce = 1.4f;  //strength per click
  public float maxSpeed = 7f;  // hard speed cap
  public float spinForce = 1f;  //spin power

  public float numberOfFlips = 1; // n number of spin rotations
  private bool spinLeft = false;
  private bool spinRight = false;
  private bool canJump = false;
  private bool canPush = false;
  private float lastPushTime = 0f;
  public float pushCooldown = 0.1f;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.centerOfMass = Vector2.zero;     // reset com to middle of skater
    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    UnityEngine.Cursor.visible = false;

  }

  void Update()
  {
    CheckInput();
    UpdateSpriteState();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (canPush && groundCheck.isGrounded && Mathf.Abs(rb.velocity.x) <= maxSpeed)
    {
      ApplyPushForce();
      canPush = false;
    }
    if (canJump)
    {
      ApplyJumpForce();
      canJump = false;
    }
  }

  void CheckInput()
    {
      if (!groundCheck.isGrounded) return;    //exit if no ground touchy

      if (Input.GetMouseButtonDown(0))
      {
        jumpTime = 0f;
      }

      if (Input.GetMouseButton(0))
      {
        jumpTime += Time.deltaTime;

        if (jumpTime > jumpTimeThreshold)
        {
          SetSpinDirection();
        }
      }

      if (Input.GetMouseButtonUp(0))
      {
        if (jumpTime <= jumpTimeThreshold)
        {
          //Debug.Log("TRY PUSH!");
          TryPushCooldown();
          StartCoroutine(SpriteSwitchPush());
        }
        else
        {
          //Debug.Log("TRY JUMP!");
          canJump = true;
          StartCoroutine(SpriteSwitchJump());
        }

        jumpTime = 0f;
      }
    }

  void SetSpinDirection()
  {
    float mouseDirection = Input.GetAxis("Mouse X");

    if (mouseDirection < -0.05f && !spinLeft)
    {
      spinLeft = true;
      spinRight = false;
      spriteRenderer.sprite = crouchLeft;
      StartCoroutine(ResetSpin());
    }
    else if (mouseDirection > 0.05f && !spinRight)
    {
      spinRight = true;
      spinLeft = false;
      spriteRenderer.sprite = crouchRight;
      StartCoroutine(ResetSpin());
    }

    else if (!spinLeft && !spinRight)
    {
      spriteRenderer.sprite = crouchSprite;
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

  void ApplyJumpForce()
    {
      rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

      float torque = 0f;
      if (spinLeft)
      {
        torque = spinForce;
      }
      else if (spinRight)
      {
        torque = -spinForce;
      }

      rb.AddTorque(spinForce * torque, ForceMode2D.Impulse);

      spinLeft = false;
      spinRight = false;
    }

  void ApplyPushForce()
    {
      float currentSpeed = Mathf.Abs(rb.velocity.x);
      float ratio = Mathf.Clamp01(1f - (currentSpeed / maxSpeed));
      float effectiveForce = pushForce * ratio;

      rb.AddForce(Vector2.right * effectiveForce, ForceMode2D.Impulse);
    }

  void UpdateSpriteState()
  {
    /*
    if (groundCheck.isGrounded && Mathf.Abs(rb.velocity.x) < 0.1f)
    {
      spriteRenderer.sprite = pushSprite;
      return;
    }
    */
  }
  

  private IEnumerator SpriteSwitchPush()
  {
    spriteRenderer.sprite = pushSprite;
    yield return new WaitForSeconds(0.1f);  //can adjust 
    if (groundCheck.isGrounded)
    {
      spriteRenderer.sprite = rideSprite;
    }
  }

  private IEnumerator SpriteSwitchJump()
  {
    spriteRenderer.sprite = jumpSprite;

    yield return new WaitUntil(() => rb.velocity.y <= 0f);
    spriteRenderer.sprite = rideSprite;

    yield return new WaitUntil(() => groundCheck.isGrounded);
    spriteRenderer.sprite = crouchSprite;

    yield return new WaitForSeconds(0.1f); //can adjust
    spriteRenderer.sprite = rideSprite;
  }

  IEnumerator ResetSpin()
  {
    yield return new WaitForSeconds(0.4f);
    spinLeft = false;
    spinRight = false;
  
  }
}




