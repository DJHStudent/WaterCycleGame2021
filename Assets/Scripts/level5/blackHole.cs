using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole : MonoBehaviour
{
    float zRot = 0, maxCheckDist = 50;

    private void Start()
    {
        ///
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.levelStats.paused)
        {
            roteObj();
            checkNearby();
        }
    }

    void roteObj()
    {
        zRot += 30 * Time.deltaTime;
        if (zRot >= 360)
            zRot = 0;
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    void checkNearby()
    {
        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, maxCheckDist);
        foreach (Collider2D coll in collision)
        {

            float dist = Mathf.Clamp(maxCheckDist - Vector2.Distance(transform.position, coll.gameObject.transform.position), 0, maxCheckDist);
            Debug.Log(dist);
            if (dist >= maxCheckDist - 8)
            {
                Destroy(coll.gameObject);
                continue;
            }
            Vector2 dir = transform.position - coll.gameObject.transform.position;
            coll.gameObject.transform.Translate(dir * .05f * dist * Time.deltaTime, Space.World);
        }
    }
}
