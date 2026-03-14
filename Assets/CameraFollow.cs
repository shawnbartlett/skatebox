using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;      //player
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothSpeed = 5f;        //follow rigitidy

    public Rigidbody2D targetRB;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
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
        
        //cam.orthographicSize = Mathf.Lerp(2f, 10f, Mathf.InverseLerp(0f, 10f, target.position.y));
        float targetSize = 2f + targetRB.velocity.magnitude * 0.5f;
        targetSize = Mathf.Clamp(targetSize, 5f, 10f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, 5f * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
