using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool paused = false;
    public static GameObject rainDrop;
    public static LevelUIManager levelUIManager;
    public static MovementManager movementManager;
    void Awake()
    {
        paused = false;
        rainDrop = GameObject.Find("RainDrop");
        levelUIManager = GetComponent<LevelUIManager>();
        movementManager = GameObject.Find("SunBeam").GetComponent<MovementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
