using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{

    float maxSpeed = 40;

    bool moving = false;
    LineRenderer sunLineRenderer, rainLineRenderer;

    private void Start()
    {
        sunLineRenderer = GetComponent<LineRenderer>();
        rainLineRenderer = GameManager.rainDrop.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.levelStats.paused)
        {
            moveSunBeam();
            moveRainDrop();
        }
    }

    void moveSunBeam() //update the sunbeams position based on input given
    {
        //update position based on key pressed
        Vector2 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -28.3f, 28.3f);
        transform.position = pos;

        //update the sunbeam
        sunLineRenderer.SetPosition(1, pos);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //if currently moving
        {
            moving = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            moving = true;
        }
        else if (moving)
        {
            moving = false;
        }
    }

    void moveRainDrop()//get the raindrop to follow the sunbeam
    {
        Vector2 rainDropPos = GameManager.rainDrop.transform.position;
        float dist = Math.Abs(transform.position.x - rainDropPos.x);//get x dist from one to the other
        //
        if (dist > 0.01)// && !moving|| Math.Abs(transform.position.x) == 28.3f)//if not too close/ not moving or at one of the boarder walls
        {
            Vector2 sunBeamPos = new Vector2(transform.position.x, rainDropPos.y);
            GameManager.rainDrop.transform.position = Vector2.MoveTowards(rainDropPos, sunBeamPos, maxSpeed * 0.7f * Time.deltaTime); //move towards new position with current speed
        }
        rainLineRenderer.SetPosition(0, GameManager.rainDrop.transform.position);
        rainLineRenderer.SetPosition(1, GameManager.rainDrop.transform.position + GameManager.rainDrop.transform.up * (10 + GameManager.levelStats.speed * 2));

        rainLookAt();
    }
    void rainLookAt()
    {
        float dir = GameManager.rainDrop.transform.position.x - transform.position.x;
        GameManager.rainDrop.transform.rotation = Quaternion.Euler(0,0,180+dir*2);
    }
}
