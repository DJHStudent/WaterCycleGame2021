using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesSpawn : MonoBehaviour
{
    public GameObject leaf;
    List<int> xSpawnPoints = new List<int>();
    int currXPos;
    public float leafSpawnTime;

    private void Start()
    {
        createSpawnPoints();
        StartCoroutine(leaveSpawn());
    }
    void createSpawnPoints() //list of all x positions to cycle through before going back and spawning in that pos again
    {
        for (int i = -30; i <= 30; i += 3)
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
    IEnumerator leaveSpawn() //repeatedly spawn in a leaf in the specified x pos at the top of the map
    {
        yield return new WaitForSeconds(leafSpawnTime);
        //spawn leaf
        Vector2 pos = new Vector2(getNext(), 54);
        Vector3 rot = new Vector3(0, 0, Random.Range(0, 360));
        GameObject currLeaf = Instantiate(leaf, pos, Quaternion.Euler(rot));
        StartCoroutine(leaveSpawn());
    }
}
