using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSide : MonoBehaviour
{
    bool direction; 
    bool randomBoolean()
{
    if (Random.value >= 0.5)
    {
        return true;
    }
    return false;
}
    protected float speed = 5;
    protected float destroyXPos = -54;

    // Update is called once per frame

    void start()
    {
        Vector2 pos = transform.position;
        if(direction == true)
        {
            pos.x = 40;
        }
        else
        {
            pos.x = -40;
        }
        transform.position = pos;
        direction = randomBoolean();

 
        

        
    }
    protected virtual void Update()
    {
        if (whenPause()) //move at the specified speed until reach level bottom
        {
            if (direction == true)
        {
          transform.Translate(Vector2.right * speed * GameManager.levelStats.speed * Time.deltaTime, Space.World); 
        }
        else
        {
           transform.Translate(Vector2.left * speed * GameManager.levelStats.speed * Time.deltaTime, Space.World); 
        }

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
