using System.Collections;
using UnityEngine;

public class PlatSpawn : MonoBehaviour
{
    float startY = 54, startWaitTime = 7f, distAppart = 70;
    public GameObject wall, end;
    GameObject newDist;//last spawned in object;

    bool canSpawn = false;

    void Start()
    {
        StartCoroutine(startWait());
    }
    void Update()
    {
        if (!GameManager.levelStats.paused && canSpawn)
        {
            float dist = Mathf.Abs(newDist.transform.position.y - startY);
            if (dist >= distAppart)
                spawn();
        }
    }

    void spawnFirst()//spawns in the first platform
    {
        if (newDist == null)
        {
            Vector2 pos = new Vector2(Random.Range(-16, 16), startY);
            GameObject gameObject = Instantiate(wall, pos, Quaternion.identity);
            newDist = gameObject;
        }
    }
    // Update is called once per frame
    void spawn()//spawn in a new platform
    {
        if (!GameManager.levelStats.paused)//fine as long as only paused when dead or made it to the end of level
        {
            Vector2 pos = new Vector2(Random.Range(-16, 16), newDist.transform.position.y + distAppart);
            GameObject gameObject = Instantiate(wall, pos, Quaternion.identity);

            newDist = gameObject;
        }
    }
    public void spawnEnd() //when at the end of the level spawn the end bar so when players collide it ends the level
    {
        //spawn the end block 70y above the curr block and stop all spawns
        Vector2 pos = new Vector2(0, newDist.transform.position.y + distAppart);
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
