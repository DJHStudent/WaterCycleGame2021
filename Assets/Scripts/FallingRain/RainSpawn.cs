using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    float startY = 54, lastXPos = 30;
    float spawnTime = 1.5f;
    public GameObject raindrop;

    List<int> xSpawnPoints = new List<int>();
    int currXPos;

    //create the list of possible X points to spawn between -30 and 30 so each 3 dist appart
    void Start()
    {
        createSpawnPoints();
        InvokeRepeating("spawn", spawnTime, spawnTime);
    }

    void createSpawnPoints()//ensures only spawns in the same location again if already spawned in all other possible positions first
    {
        for(int i = -30; i <= 30; i += 1)
        {
            xSpawnPoints.Add(i);
        }
        currXPos = xSpawnPoints.Count - 1;
    }

    public int getNext()//the next item in the list to get
    {
        if (currXPos < 0)
        {
            currXPos = xSpawnPoints.Count - 1;     
        }
        int randValue = Random.Range(0, currXPos);
        //swapp the random item and the current item
        int temp = xSpawnPoints[randValue];
        xSpawnPoints[randValue] = xSpawnPoints[currXPos];
        xSpawnPoints[currXPos] = temp;

        currXPos--;

        return temp;
    }

    // Update is called once per frame
    void spawn()//repeatidly spawn in a raindrop at the top of the level in a random x pos
    {
        if (!GameManager.levelStats.paused || GameManager.levelStats.tutActive)
        {
            float xPos = 30 * Mathf.PerlinNoise(lastXPos, startY); //Random.Range(-30, 30)
            lastXPos = xPos;
            Vector2 pos = new Vector2(getNext(), startY);
            Instantiate(raindrop, pos, Quaternion.identity);
        }
    }
}