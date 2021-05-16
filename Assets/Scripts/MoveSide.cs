using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSide : MonoBehaviour
{
    protected float speed = 20;
    protected float destroyXPos = -54;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (whenPause()) //move at the specified speed until reach level bottom
        {
            transform.Translate(Vector2.right * speed * GameManager.levelStats.speed * Time.deltaTime, Space.World);
            if (whenDestroy())
            {
                Destroy(this.gameObject);
            }
        }
    }

    protected virtual bool whenDestroy()
    {
        return transform.position.x <= destroyXPos;
    }

    protected virtual bool whenPause()
    {
        return !GameManager.levelStats.paused;
    }
}
