using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSide : MonoBehaviour
{
    bool direction; //true == left, false == right
    protected float speed = 30;
    protected float destroyXPos = -54;

    // Update is called once per frame

    void Start()
    {
        direction = randomBoolean();
        if(direction)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    protected virtual void Update()
    {
        if (whenPause()) //move at the specified speed until reach level bottom
        {
            if (!direction)
            {
                transform.Translate(Vector2.right * speed * GameManager.levelStats.speed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(Vector2.left * speed * GameManager.levelStats.speed * Time.deltaTime, Space.World);
            }
            atEdge();
            if (whenDestroy())
            {
                Destroy(this.gameObject);
            }
        }

        
    }
    void atEdge()
    {
        if(transform.position.x >= 27)
        {
            Vector2 pos = transform.position;
            pos.x = 27;
            transform.position = pos;
            transform.localScale = new Vector2(-1, 1);
            direction = true;
        }
        else if(transform.position.x <= -27)
        {
            Vector2 pos = transform.position;
            pos.x = -27;
            transform.position = pos;
            transform.localScale = new Vector2(1, 1);
            direction = false;
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
    bool randomBoolean()
    {
        return Random.value >= 0.5;
    }

}
