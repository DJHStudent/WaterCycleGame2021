using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingStats : MonoBehaviour
{
    public int currScore, currHeight;
    public float currTime, currTrust, currSize;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currScore = 0;
        currTime = 0;
        currHeight = 0;
        currTrust = 100;
        currSize = 8;
    }
}
