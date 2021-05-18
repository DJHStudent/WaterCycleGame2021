using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole : MonoBehaviour
{
    float zRot = 0, maxCheckDist = 50;

    private void Start()
    {
        Vector2 pos = transform.position;
        if (Random.value >= 0.5)
        {
            pos.x = 30;
        }
        else
            pos.x = -30;
        transform.position = pos;
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
            if (dist >= maxCheckDist - 8)
            {
                if (coll.gameObject.CompareTag("Player"))
                {
                    GameManager.levelStats.updateTrust(-100);
                    GameManager.savedInfo.soundEfxAudio.clip = GameManager.savedInfo.deadClip;
                    GameManager.savedInfo.soundEfxAudio.Play();
                    Destroy(gameObject.GetComponent<PolygonCollider2D>());
                    GameManager.levelUIManager.onDeath("The Raindrop discovered the singularity");
                }
                else
                {
                    Destroy(coll.gameObject);
                    continue;
                }
            }
            Vector2 dir = transform.position - coll.gameObject.transform.position;
            coll.gameObject.transform.Translate(dir * .05f * dist * Time.deltaTime, Space.World);
        }
    }

    bool randomBoolean()
    {
        return Random.value >= 0.5;
    }
}
