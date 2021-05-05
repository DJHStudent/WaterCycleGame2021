using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMove : MoveDown
{

    private void Start()
    {
        speed = Random.Range(20, 40);
    }

    protected override bool whenPause()
    {
        return !GameManager.levelStats.paused || GameManager.levelStats.tutActive;
    }
}
