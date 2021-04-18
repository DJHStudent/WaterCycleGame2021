using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMove : MonoBehaviour
{
    float speed = 20;
    float destroyYPos = -54;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
            if (transform.position.y <= destroyYPos)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
