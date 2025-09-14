using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideType
    {
        Tail,
        Head,
        Nose,
        Feet
    }

public class SideTrigger : MonoBehaviour
{
    public string sideName;
    public Color sideColor = Color.white;
    public bool isTouchingGround = false;

    public PhysicsMaterial2D lowFriction;
    public PhysicsMaterial2D highFriction;
    private BoxController box;
    public SideType side;
    public Collider2D bodyCollider;

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
            if (side == SideType.Feet)
            {
                bodyCollider.sharedMaterial = lowFriction;
            }
            else
            {
                bodyCollider.sharedMaterial = highFriction;
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isTouchingGround = false;
            bodyCollider.sharedMaterial = lowFriction;
            
        }

        else if (other.CompareTag("Wall)"))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

