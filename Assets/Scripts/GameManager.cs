using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject rainDrop;
    public static LevelUIManager levelUIManager;
    public static MovementManager movementManager;
    public static LevelStats levelStats;
    public static LevelGenerator levelGen;
    public static TrackingStats trackingStats;
    public static SavedInfo savedInfo;
    public static LeavesSpawn leavesSpawn;
    public static AsteroidSpawn asteroidSpawn;
    void Awake()
    {
        Application.targetFrameRate = -1;//-1 means unlimited
        Time.timeScale = 1;
        rainDrop = GameObject.Find("RainDrop");
        levelUIManager = GetComponent<LevelUIManager>();
        levelGen = GetComponent<LevelGenerator>();
        levelStats = GetComponent<LevelStats>();        
        levelStats.paused = false;
        movementManager = GameObject.Find("SunBeam").GetComponent<MovementManager>();
        trackingStats = GameObject.Find("SaveManager").GetComponent<TrackingStats>();
        savedInfo = GameObject.Find("SaveManager").GetComponent<SavedInfo>();
        if (GetComponent<LeavesSpawn>())
            leavesSpawn = GetComponent<LeavesSpawn>();

        if (GetComponent<AsteroidSpawn>())
            asteroidSpawn = GetComponent<AsteroidSpawn>();
    }
}
