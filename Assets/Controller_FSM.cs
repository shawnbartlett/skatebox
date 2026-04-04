using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_FSM : MonoBehaviour
{

    public enum SkaterState
    {
        Grounded
    }

    private Rigidbody2D rb;
  
    Animator anim;
    private SkaterState currentState;

    private bool canPush = false;
    private float jumpTime;
    private float jumpTimeThreshold = 0.2f;
    private float lastPushTime = 0f;
    public float pushCooldown = 0.1f;
    public float pushForce = 1.4f;
    public float maxSpeed = 7f;  // hard speed cap

    

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = Vector2.zero;     // reset com to middle of skater
        anim = GetComponentInChildren<Animator>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        currentState = SkaterState.Grounded;
       
    }

    void Update()
    {
        //Debug.Log(canPush);
        switch (currentState)
        {
            case SkaterState.Grounded:
                if (Input.GetMouseButtonDown(0))
                {
                    jumpTime = 0f;
                }
                if (Input.GetMouseButton(0))
                {
                    jumpTime += Time.deltaTime;
                    if (jumpTime > jumpTimeThreshold)
                    {
                        //SetSpinDirection();
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (jumpTime <= jumpTimeThreshold)
                    { 
                        if (Time.time - lastPushTime >= pushCooldown)
                        {
                            Debug.Log("PUSH!");
                            canPush = true;
                            lastPushTime = Time.time;
                        }
                    }
                    else
                    {
                        //canJump = true;  
                    }
                    jumpTime = 0f;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        if (canPush && Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                //physics stuff here
                ApplyPushForce();
                anim.SetTrigger("Push");
                //scoreManager.AddPoints(2, Color.yellow); //2 points for push
                canPush = false;
            }
    }

    void ApplyPushForce()
    {
        float scaledMaxSpeed = maxSpeed;
        float currentSpeed = Mathf.Abs(rb.velocity.x);
        float ratio = Mathf.Clamp01(1f - (currentSpeed / scaledMaxSpeed));
        float effectiveForce = pushForce * ratio;
        
        rb.AddForce(Vector2.right * effectiveForce, ForceMode2D.Impulse);

    }
}
