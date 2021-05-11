using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingStats : MonoBehaviour
{
    public int currScore, currHeight, currScene;
    public float currTime, currTrust, currSize;

    public static bool loaded = false;
    void Awake()
    {
        if (!loaded)
        {
            DontDestroyOnLoad(gameObject);
            loaded = true;
            resetStats();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void resetStats()
    {
        currScore = 0;
        currTime = 0;
        currHeight = 0;
        currTrust = 100;
        currSize = 8;
        currScene = 0;
    }
}
