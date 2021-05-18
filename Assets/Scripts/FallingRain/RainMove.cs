using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMove : MoveDown
{
    TrailRenderer trail;
    private void Start()
    {
        speed = Random.Range(25, 40);
        trail = gameObject.GetComponent<TrailRenderer>();
    }

    protected override void Update()
    {            
        base.Update();
        if (trail)
        {
            trail.time = 0.5f / GameManager.levelStats.speed;
        }
    }

    protected override bool whenPause()
    {
        return !GameManager.levelStats.paused || GameManager.levelStats.tutActive;
    }
}
