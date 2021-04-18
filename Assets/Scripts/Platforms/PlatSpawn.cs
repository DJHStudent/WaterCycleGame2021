using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatSpawn : MonoBehaviour
{
    float startY = 54;
    float spawnTime = 2;
    public GameObject wall;
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
            Vector2 pos = new Vector2(Random.Range(-16, 16), startY);
            Instantiate(wall, pos, Quaternion.identity);
        }
    }
}
