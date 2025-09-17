using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;      //player
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothSpeed = 5f;        //follow rigitidy

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (target == null) return;
        Vector3 desiredPostion = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,
         desiredPostion, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
