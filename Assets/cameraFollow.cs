using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public Transform player;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    public Camera cam;
    public float maxHeight = 500f;
    public Color startColor = Color.black;
    public Color endColor = new Color(0.4f, 0.8f, 1f);


    void LateUpdate()
    {
        SmoothFollow();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SkyColorHeightChange();
    }

    private void SmoothFollow()
    {
        if (player == null) return;

        Vector3 desiredPostion = player.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPostion, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
    }

    private void SkyColorHeightChange()
    {
        float t = Mathf.InverseLerp(0, maxHeight, player.position.y);
        cam.backgroundColor = Color.Lerp(startColor, endColor, t);
    }
}
