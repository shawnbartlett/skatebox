using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    public bool isTouchingGround = false;

   private GameManager gm;

   // Start is called before the first frame update
   void Start()
   {
      gm = FindAnyObjectByType<GameManager>();
   }

    void OnTriggerEnter2D(Collider2D other)
    {
      gm.ResetPlayer();
      isTouchingGround = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
       isTouchingGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

