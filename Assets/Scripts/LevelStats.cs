using UnityEngine;

public class LevelStats : MonoBehaviour
{
    public bool paused = false;
    public float speed = 0;
    public int score = 0;
    public float levelSecs = 180; //length of the level in seconds(chose 3 mins so all up 15 mins to do all levels and 5 mins allowed for players failing)
    public float timeLevelLoaded = 0;//length of time the level has been loaded for

    bool levelEnd = false;

    void Start()
    {
        GameManager.levelUIManager.maxHeight(levelSecs);
    }

    void Update()
    {
        if (!paused)
        {
            timeLevelLoaded += Time.deltaTime;
            GameManager.levelUIManager.updateHeight(timeLevelLoaded);
            if(timeLevelLoaded >= levelSecs && !levelEnd)
            {
                levelEnd = true;
                GameManager.platSpawn.spawnEnd();
            }
        }
    }

    public void updateScore(int amount)
    {
        score += amount;
        GameManager.levelUIManager.updateScore();
        setSpeed();
    }

    public void setSpeed()
    {
        speed = Mathf.Clamp(Mathf.Pow(1.01f, 1 + score), 0, 100); //1.005;
        Debug.Log(speed);
        Time.timeScale = speed;
    }
}
