using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject rainDrop;
    public static LevelUIManager levelUIManager;
    public static MovementManager movementManager;
    public static LevelStats levelStats;
    public static PlatSpawn platSpawn;
    void Awake()
    {
        //Application.targetFrameRate = -1;//-1 means unlimited   around 70 appart
        Time.timeScale = 1;
        rainDrop = GameObject.Find("RainDrop");
        levelUIManager = GetComponent<LevelUIManager>();
        platSpawn = GetComponent<PlatSpawn>();
        levelStats = GetComponent<LevelStats>();        
        levelStats.paused = false;
        movementManager = GameObject.Find("SunBeam").GetComponent<MovementManager>();
    }
}
