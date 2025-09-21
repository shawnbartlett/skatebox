using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform player;    //skater
    public Quaternion startRotation;
    public Vector3 startPosition;
    public float deathY = -1.0f;      // fall below reset point


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
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        player.position = startPosition;
        player.rotation = startRotation;

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
