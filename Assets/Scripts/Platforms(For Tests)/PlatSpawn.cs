using UnityEngine;

public class PlatSpawn : MonoBehaviour
{
    float startY = 54;
    float spawnTime = 3.5f;
    float distAppart = 70;
    public GameObject wall;
    public static GameObject newDist;
    // Start is called before the first frame update
    void Start()
    {
        if (newDist == null)
            newDist = this.gameObject;
        InvokeRepeating("spawn", spawnTime*2, spawnTime);
    }

    // Update is called once per frame
    void spawn()//inaccurate with differing frame rates
    {
        if (!GameManager.levelStats.paused)//fine as long as only paused when dead or made it to the end of level
        {
            Vector2 pos = new Vector2(Random.Range(-16, 16), startY);
            GameObject gameObject = Instantiate(wall, pos, Quaternion.identity);

            newDist = gameObject;
        }
    }
    public void spawnEnd() //when at the end of the level spawn the end bar so when players collide it ends the level
    {
        CancelInvoke();
    }
}
