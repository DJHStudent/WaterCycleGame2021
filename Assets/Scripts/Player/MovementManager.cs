using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    float maxSpeed = 40;
    float maxDistAway = 5;

    bool moving = false, moveLeft = false;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            moveSunBeam();
            moveRainDrop();
        }
    }

    void moveSunBeam() //update the sunbeams position based on input given
    {
        Vector2 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -28.3f, 28.3f);
        transform.position = pos;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //if currently moving
        {
            moving = true;
            if (GameManager.rainDrop.transform.position.x > transform.position.x) //if want raindrop to left of sunbeam
                moveLeft = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            moving = true;
            if (GameManager.rainDrop.transform.position.x < transform.position.x) //if want raindrop to right of sunbeam
                moveLeft = false;
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

        if (dist > 0.01 && !moving || Math.Abs(transform.position.x) == 28.3f)//if not too close/ not moving or at one of the boarder walls
        {
            Vector2 sunBeamPos = new Vector2(transform.position.x, rainDropPos.y);
            GameManager.rainDrop.transform.position = Vector2.MoveTowards(rainDropPos, sunBeamPos, maxSpeed * Time.deltaTime); //move towards new position with current speed
        }
        else if (dist >= maxDistAway && moving)//if moving and too far away
        {
            float pos = transform.position.x;
            if (!moveLeft)
            {
                pos -= maxDistAway;
            }
            else
            {
                pos += maxDistAway;
            }
            GameManager.rainDrop.transform.position = new Vector2(pos, rainDropPos.y);//ensures the raindrop never gets more then maxDistAway from the sunbeam
        }

    }
}
