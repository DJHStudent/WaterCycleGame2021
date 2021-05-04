using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject rainDrop;
    public static LevelUIManager levelUIManager;
    public static MovementManager movementManager;
    public static LevelStats levelStats;
    public static LevelGenerator levelGen;
    public static TrackingStats trackingStats;
    void Awake()
    {
        //Application.targetFrameRate = -1;//-1 means unlimited
        Time.timeScale = 1;
        rainDrop = GameObject.Find("RainDrop");
        levelUIManager = GetComponent<LevelUIManager>();
        levelGen = GetComponent<LevelGenerator>();
        levelStats = GetComponent<LevelStats>();        
        levelStats.paused = false;
        movementManager = GameObject.Find("SunBeam").GetComponent<MovementManager>();
        trackingStats = GameObject.Find("SaveManager").GetComponent<TrackingStats>();
    }
}
