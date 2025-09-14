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
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isTouchingGround = false;
            
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

