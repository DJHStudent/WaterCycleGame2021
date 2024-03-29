using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMoveSide : MonoBehaviour
{

    protected float speed = 31;
    protected float destroyXPos = 90;
    int direction;
    

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x >= 30)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (whenPause()) //move at the specified speed until reach level bottom
        {
            transform.Translate(transform.right * direction * speed * Time.deltaTime, Space.World);
            if (transform.position.x >= destroyXPos || transform.position.x <= -destroyXPos)
            {
                Destroy(this.gameObject);
            }
        }
    }

    protected virtual bool whenPause()
    {
        return !GameManager.levelStats.paused;
    }
}
