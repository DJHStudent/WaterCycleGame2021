using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawn : MonoBehaviour
{
    float startY = 54, lastXPos = 30;
    public float spawnTime = 1.5f;
    public GameObject raindrop;
    public bool tuteSpawn = false;

    List<int> xSpawnPoints = new List<int>();
    int currXPos;

    //create the list of possible X points to spawn between -30 and 30 so each 3 dist appart
    void Start()
    {
        createSpawnPoints();
        StartCoroutine(rainSpawn());
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

    void spawn()//repeatidly spawn in a raindrop at the top of the level in a random x pos
    {
        Debug.Log(GameManager.levelStats.paused);
        if (!GameManager.levelStats.paused || GameManager.levelStats.tutActive && tuteSpawn)
        {
            Debug.Log("spawn");
            float xPos = 30 * Mathf.PerlinNoise(lastXPos, startY); //Random.Range(-30, 30)
            lastXPos = xPos;
            Vector2 pos = new Vector2(getNext(), startY);
            Instantiate(raindrop, pos, Quaternion.Euler(180, 0, 0));
        }
    }

    IEnumerator rainSpawn()
    {
        Debug.Log(GameManager.levelStats.speed);
        yield return new WaitForSeconds(spawnTime / GameManager.levelStats.speed);
        spawn();
        StartCoroutine(rainSpawn());

    }
}