using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    protected float speed;
    protected float destroyYPos = -54;

    // Update is called once per frame
    void Update()
    {
        if (whenPause()) //move at the specified speed until reach level bottom
        {
            transform.Translate(Vector2.down * speed * GameManager.levelStats.speed * Time.deltaTime, Space.World);
            if (whenDestroy())
            {
                Destroy(this.gameObject);
            }
        }
    }

    protected virtual bool whenDestroy()
    {
        return transform.position.y <= destroyYPos;
    }

    protected virtual bool whenPause()
    {
        return !GameManager.levelStats.paused;
    }
}
