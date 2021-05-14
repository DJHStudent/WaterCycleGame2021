using UnityEngine;

public class LevelStats : MonoBehaviour
{
    public bool paused = false, tutActive = false, notrust = false;
    public float speed = 1, playerTrust = 100, playerSize = 8;
    public int score = 0;
    public float levelSecs = 180; //length of the level in seconds(chose 3 mins so all up 15 mins to do all levels and 5 mins allowed for players failing)
    public float timeLevelLoaded = 0;//length of time the level has been loaded for

    public string currLevel, nextLevel;

    bool levelEnd = false;

    void Start()
    {
        updateScore(GameManager.trackingStats.currScore);
        updateTrust(-(100 - GameManager.trackingStats.currTrust));
        updateSize(-(8 - GameManager.trackingStats.currSize));
        GameManager.levelUIManager.maxHeight(levelSecs);
    }

    void Update()
    {
        if (!paused)
        {
            updateTrust(Time.deltaTime * 2);//trust regen rate
            timeLevelLoaded += Time.deltaTime * speed;//deictates the height the player currently at
            GameManager.levelUIManager.updateHeight(timeLevelLoaded);
            updateSize(-Time.deltaTime * 0.25f); //slowly loose mass over time
            if(timeLevelLoaded >= levelSecs && !levelEnd)//if reach the max height for the level
            {
                levelEnd = true;
                GameManager.levelGen.spawnEnd();
            }
        }
    }

    public void updateScore(int amount)
    {
        score += amount;
        GameManager.levelUIManager.updateScore();
        setSpeed();
    }

    public void updateTrust(float value)
    {
        playerTrust += value;
        playerTrust = Mathf.Clamp(playerTrust, 0, 100);
        GameManager.levelUIManager.setTrust(playerTrust);
    }

    public void updateSize(float value)//update the amount of water the player has left
    {
        playerSize += value;
        playerSize = Mathf.Clamp(playerSize, 0, 8);//the min and max possible size for the player
        GameManager.rainDrop.transform.localScale = new Vector2(playerSize, playerSize);
        if(playerSize <= 0)
        {
            GameManager.levelUIManager.onDeath("The Raindrop Evaporated");
        }
    }
    public void setSpeed()//set the games speed exponentially based on the players score
    {
        speed = Mathf.Clamp(Mathf.Pow(1 + score, 0.1f), 0, 3.5f); //1.01; //actually need further looking into
        //Time.timeScale = speed;
    }

    public void saveBestStats()
    {
        if (score * 100 > GameManager.savedInfo.highScore)
            GameManager.savedInfo.highScore = score * 100;

        int height = GameManager.trackingStats.currHeight + Mathf.RoundToInt(GameManager.levelStats.timeLevelLoaded / 5);
        if (height > GameManager.savedInfo.maxHight)
            GameManager.savedInfo.maxHight = height;
    }
}
