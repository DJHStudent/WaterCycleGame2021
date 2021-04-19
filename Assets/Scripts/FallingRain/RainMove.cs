using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMove : MonoBehaviour
{
    float speed = 30;
    float destroyYPos = -54;

    // Update is called once per frame
    private void Start()
    {
        speed = Random.Range(20, 40);
    }
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
