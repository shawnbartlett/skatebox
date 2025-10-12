using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPunch : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Punch());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Punch()
    {
        transform.localScale *= 1.3f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale /= 1.3f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * 0.2f;
        //transform.position += Vector3.up * 0.2f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 8f;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        Destroy(gameObject, 0.1f);

    }
}
