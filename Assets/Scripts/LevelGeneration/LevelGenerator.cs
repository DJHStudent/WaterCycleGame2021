using System.Collections;
using UnityEngine;

[System.Serializable]
public class ObjStats
{
    public GameObject obj; // the object spawining in
    public float minX, maxX; // the objects min/max X pos 
    //public int spawnChance;//essentially the number of each item you want to appear in a list of 20

}

[System.Serializable]
public class LevelPacing
{
    public int spawnChance; // the object spawining in
    public float leaveSpawnRate; // the rate at which the leaves spawn in, in seconds
    public int distPlatsAppart;
    public int[] platformSpawnChance; // the objects min/max X pos 
}
public class LevelGenerator : MonoBehaviour
{
    protected float spawnY = 54;
    public float startWaitTime, leafSpawnTime, distAppart, xDistNotSpawn; //distance where the gap occurs where it cannot spawn
    public ObjStats[] wall; //a list of all the possible platforms which can spawn in
    public LevelPacing[] levelPacing; //for this one the first dimetntion is the type pacing you want spawned in, 2nd dimention is the number of each platform(in order) you want to appear
    public GameObject end, leaf; //the end object
    public Transform newDist;//last spawned in platforms position;

    protected bool canSpawn = false;//can a platform spawn or not

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
        leafSpawnTime = levelPacing[0].leaveSpawnRate;
        StartCoroutine(startWait());
        StartCoroutine(leaveSpawn());
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
        int pos;
        Debug.Log("change bag using");
        if (currBag == hardCoreSpawn)
            pos = 0;
        else
            pos = spawnType.getNext();
        switch (pos)
        {
            case 0:
                currBag = exploreSpawn;
                distAppart = levelPacing[pos].distPlatsAppart;
                leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                break;
            case 1:
                currBag = midGroundSpawn;
                distAppart = levelPacing[pos].distPlatsAppart;
                leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                break;
            case 2:
                currBag = bottleNeckSpawn;
                distAppart = levelPacing[pos].distPlatsAppart;
                leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                break;
            default: 
                currBag = hardCoreSpawn;
                distAppart = levelPacing[pos].distPlatsAppart;
                leafSpawnTime = levelPacing[pos].leaveSpawnRate;
                break;
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
            ObjStats newObj = determineObj();
            Vector2 pos = new Vector2(objXPos(newObj), spawnY);
            GameObject gameObject = Instantiate(newObj.obj, pos, Quaternion.identity);
            newDist = gameObject.transform;
        }
    }
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

    IEnumerator leaveSpawn()
    {
        yield return new WaitForSeconds(leafSpawnTime);
        //spawn leaf
        Vector2 pos = new Vector2(Random.Range(-30, 30), 54);
        Instantiate(leaf, pos, Quaternion.identity);
        StartCoroutine(leaveSpawn());
    }

    //leaf spawning now can finally do
}
