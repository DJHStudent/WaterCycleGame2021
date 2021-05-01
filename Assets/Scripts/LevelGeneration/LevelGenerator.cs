using System.Collections;
using UnityEngine;

[System.Serializable]
public class ObjStats
{
    public GameObject obj; // the object spawining in
    public float minX, maxX; // the objects min/max X pos 
    public int spawnChance;//the number of each item you want to appear in the shuffle list e.g if x = 100 and a = 1 it would display 100 x and 1 a before another a could be displayed
    //a number between 1 and 10(inclusive), with that amount being placed in a list, then use a shuffle bag

}
public class LevelGenerator : MonoBehaviour
{
    protected float spawnY = 54, startWaitTime, distAppart;
    public ObjStats[] wall; //a list of all the possible platforms which can spawn in(ordered from most to least likely)
    public GameObject end; //the end object
    protected Transform newDist;//last spawned in platforms position;

    protected bool canSpawn = false;//can a platform spawn or not

    ShuffleBag bag;

    protected virtual void Start()
    {
        bag = new ShuffleBag();
        bag.initilize();
        StartCoroutine(startWait());
    }
    void Update()
    {
        if (!GameManager.levelStats.paused && canSpawn)
        {
            float dist = Mathf.Abs(newDist.position.y - spawnY);
            if (dist >= distAppart)//if last platform far enought from its spawn position
                spawn();
        }
    }
    //////implement a shuffle bag approach to get the best possible random results
    ObjStats determineObj() //here for reference https://docs.unity3d.com/2019.3/Documentation/Manual/RandomNumbers.html 
    {
        return wall[bag.getNext()];
    }

    float objXPos(ObjStats pos)//gets a random x position to spawn the object in
    {
        return Random.Range(pos.minX, pos.maxX);
    }



    void spawnFirst()//spawns in the first platform
    {
        if (newDist == null)
        {
            ObjStats newObj = determineObj();
            Vector2 pos = new Vector2(objXPos(newObj), spawnY);
            GameObject gameObject = Instantiate(newObj.obj, pos, Quaternion.identity);
            newDist = gameObject.transform;
        }
    }
    // Update is called once per frame
    void spawn()//spawn in a new platform so its distAppart from the last platform
    {
        if (!GameManager.levelStats.paused)
        {
            ObjStats newObj = determineObj();
            Vector2 pos = new Vector2(objXPos(newObj), newDist.position.y + distAppart);
            GameObject gameObject = Instantiate(newObj.obj, pos, Quaternion.identity);
            newDist = gameObject.transform;
        }
    }
    public void spawnEnd() //when at the end of the level spawn the end bar so when players collide with it they go to the next level
    {
        //spawn the end block 70y above the curr block and stop all spawns
        Vector2 pos = new Vector2(0, newDist.position.y + distAppart);
        Instantiate(end, pos, Quaternion.identity);
        canSpawn = false;
    }

    IEnumerator startWait()//wait x seconds before begining to spawn in the platforms
    {
        yield return new WaitForSeconds(startWaitTime);
        canSpawn = true;
        spawnFirst();

    }
}