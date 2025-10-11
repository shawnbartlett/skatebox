using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
   public bool isTouchingGround = false;
   private SkateControllerFSM skater;
   private GameManager gm;

   // Start is called before the first frame update
   void Start()
   {
      gm = FindAnyObjectByType<GameManager>();
      skater = GetComponentInParent<SkateControllerFSM>();
   }

    void OnTriggerEnter2D(Collider2D other)
    {
      
      skater.Bonk();
      //gm.ResetPlayer();
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

