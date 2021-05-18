using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameObject leaf;
    public GameObject ufo;
    List<int> xSpawnPoints = new List<int>();
    int currXPos;
    public float leafSpawnTime;

    private void Start()
    {
        createSpawnPoints();
        StartCoroutine(leaveSpawn());
        StartCoroutine(spaceshipSpawn());
    }
    void createSpawnPoints() //list of all x positions to cycle through before going back and spawning in that pos again
    {
        for (int i = -30; i <= 30; i += 3)
        {
            if(i == -30 || i == 30)
            {
                xSpawnPoints.Add(i);
            }
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
    IEnumerator leaveSpawn() //repeatedly spawn in a leaf in the specified x pos at the top of the map
    {
        yield return new WaitForSeconds(leafSpawnTime / GameManager.levelStats.speed);
        //spawn leaf
        if (!GameManager.levelStats.paused)
        {
            int next_x = 0;
            next_x = getNext();
            Vector2 pos = new Vector2(getNext(), 54);
            Vector3 rot = new Vector3(0, 0, Random.Range(0, 360));
            Instantiate(leaf, pos, Quaternion.Euler(rot));
        }
        StartCoroutine(leaveSpawn());
    }
    IEnumerator spaceshipSpawn()
    {
        yield return new WaitForSeconds(leafSpawnTime / GameManager.levelStats.speed);

        if (!GameManager.levelStats.paused)
        {

            int a = -84, s = 1;
            var val = Random.value;

            if (val < 0.5f)
            {
                a = -84;
                s = 1;
            }
            else if (val > 0.5f)
            {
                a = 84;
                s = -1;
            }
            Vector2 pos = new Vector2(a, Random.Range(-45,45));
            Vector3 rot = new Vector3(0, 0, Random.Range(-30, 30));
            GameObject u = Instantiate(ufo, pos, Quaternion.Euler(rot));
            u.transform.localScale = new Vector3(30 * s,30,0);


        }
        StartCoroutine(spaceshipSpawn());
    }
}
