using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//also move leaves so spawns with own script, ensuring can be not enabled if on tutorial
[System.Serializable]
public class ObjStats
{
    public GameObject obj; // the object spawining in
    public float minX, maxX; // the objects min/max X pos 
}

[System.Serializable]
public class LevelPacing
{
    public int spawnChance; //the number of each of the 4 difficulty types you want to have spawn in before it repeates
    public float leaveSpawnRate; //the rate at which the leaves spawn in, in seconds for this difficulty
    public int distPlatsAppart; //the platforms distance appart for this difficulty
    public int[] platformSpawnChance; // the for each of the platforms identified from wall what is the spawn chance of each one to actually spawn in(in same order seen in wall)
}
public class LevelGenerator : MonoBehaviour
{
    protected float spawnY = 54;
    public float startWaitTime;
    [HideInInspector] public float distAppart; 
    float xDistNotSpawn = 10; //distance where the gap occurs where it cannot spawn
    public ObjStats[] wall; //a list of all the possible platforms which can spawn in
    public LevelPacing[] levelPacing; //list of the 4 different difficulty options
    public GameObject end; //the end object
    [HideInInspector] public Transform newDist;//last spawned in platforms position;

    protected bool canSpawn = false;//can a platform spawn or not
    bool changeDist = false; float newDistAppart;

    ShuffleBag spawnType, exploreSpawn, midGroundSpawn, bottleNeckSpawn, hardCoreSpawn;//the random list of platforms to choose from
    ShuffleBag currBag;

    protected virtual void Start()
    {
        //bag = new ShuffleBag(); bag.initilize();

        spawnType = new ShuffleBag(); spawnType.createTypes();
        exploreSpawn = new ShuffleBag(); exploreSpawn.createList(0);
        midGroundSpawn = new ShuffleBag(); midGroundSpawn.createList(1);
        bottleNeckSpawn = new ShuffleBag(); bottleNeckSpawn.createList(2);
        hardCoreSpawn = new ShuffleBag(); hardCoreSpawn.createList(3);

        currBag = exploreSpawn;
        distAppart = levelPacing[0].distPlatsAppart;
    }
    public void initialiseGeneration()
    {
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
    
    ObjStats determineObj() //here for reference https://docs.unity3d.com/2019.3/Documentation/Manual/RandomNumbers.html 
    {
        return wall[currBag.getNext()];
    }

    public void updateBag()//change the shufflebag using
    {
        if (canSpawn)
        {
            int pos;
            Debug.Log("Changed Bag Using");
            if (currBag == hardCoreSpawn)
                pos = 0;
            else
                pos = spawnType.getNext();
            switch (pos)
            {
                case 0:
                    currBag = exploreSpawn;
                    newDistAppart = levelPacing[pos].distPlatsAppart;
                    changeDist = true;
                    if (GameManager.leavesSpawn) //leaves exists as something which can be spawned in
                        GameManager.leavesSpawn.leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                    break;
                case 1:
                    currBag = midGroundSpawn;
                    newDistAppart = levelPacing[pos].distPlatsAppart;
                    changeDist = true;
                    if (GameManager.leavesSpawn) //leaves exists as something which can be spawned in
                        GameManager.leavesSpawn.leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                    break;
                case 2:
                    currBag = bottleNeckSpawn;
                    newDistAppart = levelPacing[pos].distPlatsAppart;
                    changeDist = true;
                    if (GameManager.leavesSpawn) //leaves exists as something which can be spawned in
                        GameManager.leavesSpawn.leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                    break;
                default:
                    currBag = hardCoreSpawn;
                    newDistAppart = levelPacing[pos].distPlatsAppart;
                    changeDist = true;
                    if (GameManager.leavesSpawn) //leaves exists as something which can be spawned in
                        GameManager.leavesSpawn.leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                    break;
            }
        }
    }

    float objXPos(ObjStats pos)//gets a random x position to spawn the object in so not give a gap over the same spot as last time
    {
        if (newDist == null)
            return Random.Range(pos.minX, pos.maxX);
        //ensures the randomly chosen value is not between the gap of the previous one
        float maxMinVal = newDist.position.x - xDistNotSpawn;
        float minMaxVal = newDist.position.x + xDistNotSpawn;
        
        float[] posValues = new float[2];
        posValues[0] = Random.Range(pos.minX, maxMinVal);
        posValues[1] = Random.Range(minMaxVal, pos.maxX);
        //ensure the value returned is still within the appropriate range
        if (maxMinVal < pos.minX)
            return posValues[1];
        if (minMaxVal > pos.maxX)
            return posValues[0];
        return posValues[Random.Range(0, 2)];
    }

    void spawnFirst()//spawns in the first platform
    {
        if (newDist == null)
        {
            if (GameManager.leavesSpawn) //set the rate of the leaves spawning to the exploration rate
                GameManager.leavesSpawn.leafSpawnTime = levelPacing[0].leaveSpawnRate;

            ObjStats newObj = determineObj();
            Vector2 pos = new Vector2(objXPos(newObj), spawnY);
            GameObject gameObject = Instantiate(newObj.obj, pos, Quaternion.identity);
            newDist = gameObject.transform;
        }
    }
    void spawn()//spawn in a new platform so its distAppart from the last platform
    {
        if (!GameManager.levelStats.paused && canSpawn)
        {
            ObjStats newObj = determineObj();
            Vector2 pos = new Vector2(objXPos(newObj), newDist.position.y + distAppart);
            if (changeDist)
            {
                distAppart = newDistAppart;
                changeDist = false;
            }
            GameObject gameObject = Instantiate(newObj.obj, pos, Quaternion.identity);
            newDist = gameObject.transform;
        }
    }
    public void spawnEnd() //when at the end of the level spawn the end bar so when players collide with it they go to the next level
    {
        //spawn the end block 70y above the curr block and stop all spawns
        Vector2 pos = new Vector2(0, newDist.position.y + distAppart);
        GameObject gameObject = Instantiate(end, pos, Quaternion.identity);
        newDist = gameObject.transform;
        canSpawn = false;
    }

    IEnumerator startWait()//wait x seconds before begining to spawn in the platforms
    {
        yield return new WaitForSeconds(startWaitTime / GameManager.levelStats.speed);
        canSpawn = true;
        spawnFirst();

    }
}
