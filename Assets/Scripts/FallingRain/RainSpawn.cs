using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawn : MonoBehaviour
{
    float startY = 54;
    float spawnTime = 1.5f;
    public GameObject raindrop;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void spawn()
    {
        if (!GameManager.paused)
        {
            Vector2 pos = new Vector2(Random.Range(-16, 16), Camera.main.transform.position.y + startY);
            Instantiate(raindrop, pos, Quaternion.identity);
        }
    }
}
