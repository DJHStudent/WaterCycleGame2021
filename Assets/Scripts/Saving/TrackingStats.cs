using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingStats : MonoBehaviour
{
    public int currScore, currHeight, currScene;
    public float currTime, currTrust, currSize;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        resetStats();
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
