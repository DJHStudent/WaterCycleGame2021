using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool paused = false;
    public static GameObject rainDrop;
    public static LevelUIManager levelUIManager;
    void Awake()
    {
        paused = false;
        rainDrop = GameObject.Find("RainDrop");
        levelUIManager = GetComponent<LevelUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
