using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawn : MonoBehaviour
{
    float startY = 54;
    float spawnTime = 1.5f;
    public GameObject raindrop;
    void Start()
    {
        InvokeRepeating("spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void spawn()//repeatidly spawn in a raindrop at the top of the level in a random x pos
    {
        if (!GameManager.levelStats.paused)
        {
            Vector2 pos = new Vector2(Random.Range(-30, 30), Camera.main.transform.position.y + startY);
            Instantiate(raindrop, pos, Quaternion.identity);
        }
    }
}
