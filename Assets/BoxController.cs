using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{
  [SerializeField] float launchForce = 50.0f;
  [SerializeField] private float failVelocityThreshhold = -0.1f;
  private bool hasLaunched = false;
  private bool hasFailed = false;

  public float maxChargeTime = 3.0f;

  private float chargeTimer = 0f;
  public Color baseColor = Color.white;
  public Color chargedColor = Color.yellow;
  private SpriteRenderer sr;

  private Rigidbody2D rb;
  private bool isCharging = false;

  public bool canJump; // sides
  private bool hasSquished = true;
  private float dotProductBox;
  private bool boxSquishUp;

  public SideTrigger bottomSide;
  public SideTrigger leftSide;
  public SideTrigger rightSide;
  public SideTrigger topSide;

  public ParticleSystem chargeParticles;


  private Vector3 originalScale;


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    originalScale = transform.localScale;
    sr = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    setDotProduct();
    CheckGroundedSide();
    ChargeAndLaunch();

  }

  // Update is called once per frame
  void FixedUpdate()
  {
    CheckIfFailed();
  }

  IEnumerator StrechOnJump()
  {

    transform.localScale = boxSquishUp ? new Vector3(0.8f, 1.2f, 1f) :
      new Vector3(1.2f, 0.8f, 1f);
    yield return new WaitForSeconds(0.15f);
    transform.localScale = originalScale;
  }

  public IEnumerator SquishOnLand()
  {
    if (hasSquished) yield break;

    transform.localScale = boxSquishUp ? new Vector3(1f, 0.8f, 1f) :
      new Vector3(0.8f, 1f, 1f);
    yield return new WaitForSeconds(0.05f);
    transform.localScale = originalScale;
    hasSquished = true;
  }


  public void resetHasSquished()
  {
    hasSquished = false;
  }

  void setDotProduct()
  {
    dotProductBox = Vector3.Dot(transform.up, Vector3.up);
    boxSquishUp = dotProductBox >= 0.5f | dotProductBox < -0.5f ? true : false;
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

  private void ChargeAndLaunch()
  {
    float chargeRatio = chargeTimer / maxChargeTime;

    if (Input.GetKey(KeyCode.Space) && isGrounded())
    {

      isCharging = true;
      chargeTimer += Time.deltaTime;
      chargeTimer = Mathf.Min(chargeTimer, maxChargeTime);
      sr.color = Color.Lerp(baseColor, chargedColor, chargeRatio);

    }
    if (Input.GetKeyUp(KeyCode.Space) && isCharging)
    {

      Vector2 launchVector = Vector2.up * chargeRatio * launchForce;

      rb.velocity = Vector2.zero;
      rb.AddForce(launchVector, ForceMode2D.Impulse);

      chargeTimer = 0f;
      isCharging = false;
      hasLaunched = true;
      sr.color = Color.Lerp(chargedColor, baseColor, chargeRatio);

    }
  }
  
  private void AutoLaunchBox()
  {
    //automatically launch
    float chargeRatio = chargeTimer / maxChargeTime;
    isCharging = true;
    chargeTimer += Time.deltaTime;
    chargeTimer = Mathf.Min(chargeTimer, maxChargeTime);

    Vector2 launchVector = Vector2.up * chargeRatio * launchForce;

    rb.velocity = Vector2.zero;
    rb.AddForce(launchVector, ForceMode2D.Impulse);

    chargeTimer = 0f;
    isCharging = false;

  }
  private void CheckIfFailed()
  {
    if (hasLaunched && !hasFailed)
    {
      if (rb.velocity.y < failVelocityThreshhold)
      {
        hasFailed = true;
        Debug.Log("FAIL");
      }
    }
  }
}



