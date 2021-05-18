using System.Collections;
using UnityEngine;

public class Level1Gen : LevelGenerator
{
    protected override void Start()
    {
        startWaitTime = 7;
        distAppart = 70;
        base.Start();
    }
    
}
