using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SkateControllerFSM skater;
    public Transform player;    //skater
    public Quaternion startRotation;
    public Vector3 startPosition;
    public ScoreManager scoreManager;
    public float deathY = -100.0f;      // fall below reset point


    // Start is called before the first frame update
    void Start()
    {
        startPosition = player.position;
        startRotation = player.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y < deathY)
        {
            //ResetPlayer();
        }

        if (Input.GetKeyDown("k"))
        {
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        skater.SetState(SkateControllerFSM.SkaterState.Death);
        //Debug.Log("YOU DIED!");
        player.position = startPosition;
        player.rotation = startRotation;

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        
        scoreManager.PlayerDeath();
    }
}
