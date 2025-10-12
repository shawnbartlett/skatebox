using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateControllerFSM : MonoBehaviour
{

    public enum SkaterState
    {
        Grounded,
        Airborne,
        Crashed
    }

    private SkaterState currentState;
    public ScoreManager scoreManager;
    private Collider2D boardCollider;
    private Collider2D playerCollider;
    public PhysicsMaterial2D normalFriction;
    public PhysicsMaterial2D crashFriction;

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
    private float airInputTime;
    private float landingAssistThreshold = 0.2f;
    private bool landingAssistActive = false;
    public float jumpForce = 5f;
    public float pushForce = 1.4f;  //strength per click
    public float maxSpeed = 7f;  // hard speed cap
    public float spinForce = 1f;  //spin power
    public float spinSlowDownStrength = 5f;  //magnetize slow 
    private float airScoreTimer = 0f;


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

        currentState = SkaterState.Grounded;
        playerCollider = GetComponent<Collider2D>();
        boardCollider = GetComponentInChildren<Collider2D>();
        boardCollider.sharedMaterial = normalFriction;
    }

    void Update()
    {
        

        //Debug.Log(currentState);

        switch (currentState)
        {
            case SkaterState.Grounded:
                if (!groundCheck.isGrounded)
                {
                    currentState = SkaterState.Airborne;
                }
                HandleGrounded();
                break;

            case SkaterState.Airborne:
                if (groundCheck.isGrounded)
                {
                    scoreManager.BankPoints();
                    currentState = SkaterState.Grounded;
                    airScoreTimer = 0f;
                }
                HandleAirborne();
                break;

            case SkaterState.Crashed:
                boardCollider.sharedMaterial = crashFriction;
                HandleCrashRecovery();
                break;
        }
    }

    void FixedUpdate()
    {
        if (currentState == SkaterState.Grounded)
        {
            float scaledMaxSpeed = maxSpeed + (scoreManager.totalScore * 1f);
            if (canPush && Mathf.Abs(rb.velocity.x) < scaledMaxSpeed)
            {
                //physics stuff here
                ApplyPushForce();
                scoreManager.AddPoints(2); //2 points for push
                canPush = false;
            }

            if (canJump)
            {
                ApplyJumpForce();
                canJump = false;
                landingAssistActive = true;
            }
        }

        else if (currentState == SkaterState.Airborne)
        {
            airScoreTimer += Time.deltaTime;
            if (airScoreTimer >= 0.2f)
            {
                scoreManager.AddPoints(1); //add points 
                airScoreTimer = 0f;
            }
            

        }
    }

    void HandleGrounded()
    {
        HandleGroundInput();
    }

    void HandleAirborne()
    {
        if (Input.GetMouseButtonDown(0))
        {
            airInputTime = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            airInputTime += Time.deltaTime;

            if (airInputTime > landingAssistThreshold)
            {
                //landingAssistActive = true;
            }
        }

        else
        {
            //landingAssistActive = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (airInputTime <= landingAssistThreshold)
            {
                //DoTrick();
                
            }

            airInputTime = 0f;
        }
    }


    void HandleGroundInput()
    {

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
        float scaledMaxSpeed = maxSpeed + (scoreManager.totalScore * 1f);
        float currentSpeed = Mathf.Abs(rb.velocity.x);
        float ratio = Mathf.Clamp01(1f - (currentSpeed / scaledMaxSpeed));
        float effectiveForce = pushForce * ratio;

        rb.AddForce(Vector2.right * effectiveForce, ForceMode2D.Impulse);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!landingAssistActive) return;

        rb.angularVelocity = 0f;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 0.4f);
    }

    public void Bonk()
    {   
        landingAssistActive = false;
        currentState = SkaterState.Crashed;
        scoreManager.ResetCombo();
        rb.angularVelocity = 0f;
        rb.drag = 0.1f;
        rb.angularDrag = 0.1f;
    }

    private void HandleCrashRecovery()
    {

        if (rb.velocity.magnitude < 0.2f)
        {
            StartCoroutine(ResetCharacterAfterCrash());
            StartCoroutine(CharacterResetBlink());
        }
    }

    private IEnumerator ResetCharacterAfterCrash()
    {
        rb.angularVelocity = 0f;
        boardCollider.enabled = false;
        playerCollider.enabled = false;
        rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);  //can adjust
        rb.rotation = 0f;

        rb.drag = 0.01f;
        rb.angularDrag = 0.02f;

        boardCollider.enabled = true;
        playerCollider.enabled = true;
        boardCollider.sharedMaterial = normalFriction;
        landingAssistActive = true;
        currentState = SkaterState.Grounded;

    }
    private IEnumerator CharacterResetBlink()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }
    }
    
}
