using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    float maxSpeed = 40;
    float rainDropSpeedIntval = .23f;
    public float rainDropSpeed = 0;
    float maxDistAway = 5;

    bool moving = false, moveLeft = false;

    // Update is called once per frame
    void Update()
    {
        moveSunBeam();
        moveRainDrop();
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

        if (dist < 0.01 && !moving)
            rainDropSpeed = 0;//not moving
        else if (!moving)//dist < maxDistAway if not too far away or currently not moving
        {
            rainDropSpeed += maxSpeed * Time.deltaTime;//increase the movement speed
            rainDropSpeed = Mathf.Clamp(rainDropSpeed, -rainDropSpeedIntval, rainDropSpeedIntval);//clamp it between two values
            Vector2 sunBeamPos = new Vector2(transform.position.x, rainDropPos.y);
            GameManager.rainDrop.transform.position = Vector2.MoveTowards(rainDropPos, sunBeamPos, rainDropSpeed); //move towards new position with current speed
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
