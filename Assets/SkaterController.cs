using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SkaterController : MonoBehaviour
{

  public float jumpForce = 5f;
  public float pushForce = 1.4f;  //strength per click
  public float maxSpeed = 7f;  // hard speed cap

  public GroundTrigger groundCheck;

  private Rigidbody2D rb;
  public SpriteRenderer spriteRenderer;
  public Sprite rideSprite;
  public Sprite pushSprite;
  public Sprite crouchSprite;
  public Sprite jumpSprite;

  private bool wantsToPush = false;
  private float lastPushTime = 0f;
  public float pushCooldown = 0.1f;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  
  }

  void Update()
  {
    CheckPushInput();
    UpdateSpriteState();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (wantsToPush && groundCheck.isGrounded && Mathf.Abs(rb.velocity.x) <= maxSpeed)
    {
      ApplyPushForce();
    }

    wantsToPush = false;

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
      wantsToPush = true;
      lastPushTime = Time.time;
    }
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
    if (Time.time < lastPushTime + pushCooldown)
    {
      spriteRenderer.sprite = pushSprite;
      return;
    }
    if (Mathf.Abs(rb.velocity.x) < 0.1f)
    {
      spriteRenderer.sprite = pushSprite;
      return;
    }
    spriteRenderer.sprite = rideSprite;
  }

}




