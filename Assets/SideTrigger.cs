using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideType
    {
        Red,
        Blue,
        Green,
        Yellow
    }

public class SideTrigger : MonoBehaviour
{

    public string sideName;
    public Color sideColor = Color.white;
    public bool isTouchingGround = false;

    //private float chargeTime = 0f;
    public float maxChargeTime = 2f;
    public bool isSliding = false;

    private BoxController box;

    public SideType sideType;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponentInParent<BoxController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isTouchingGround = true;
            //Debug.Log("Touching Ground");
            //box.SetActiveColor(sideColor);
            box.StartCoroutine(box.SquishOnLand());
            box.canJump = true;

        }
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isTouchingGround = false;
            //box.jumpParticles.Play();
            box.resetHasSquished();
        }

        else if (other.CompareTag("Wall)"))
        {
            isSliding = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

