using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float maxSpeed;
    float sunbeamSpeed;
    public GameObject rainDrop;
    float rainDropSpeedIntval = .1f;
    public float rainDropSpeed = 0;

    bool moving = false, moveLeft = false;

    // Update is called once per frame
    void Update()
    {
        moveSunBeam();
        moveRainDrop();
    }

    void moveSunBeam()
    {
        Vector2 pos = transform.position;
        sunbeamSpeed = Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;
        pos.x += sunbeamSpeed;
        pos.x = Mathf.Clamp(pos.x, -30, 30);
        transform.position = pos;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moving = true;
            if (rainDrop.transform.position.x > transform.position.x)
                moveLeft = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {//if a keypressed started moving
            moving = true;
            if (rainDrop.transform.position.x < transform.position.x)
                moveLeft = false;
        }
        else if (moving)
        {
            moving = false;
        }
    }

    void moveRainDrop()
    {
        float dist = Math.Abs(transform.position.x - rainDrop.transform.position.x);

        Debug.Log(rainDropSpeed +"  " + dist);

        if (dist < 0.01 && !moving)
            rainDropSpeed = 0;
        else// if (dist <= 10 || !moving)// || moving)//if not too close or currently moving
        {
            rainDropSpeed += rainDropSpeedIntval * Time.deltaTime;
            rainDropSpeed = Mathf.Clamp(rainDropSpeed, -0.1f, 0.1f);
            Vector2 sunBeamPos = new Vector2(transform.position.x, rainDrop.transform.position.y);
            rainDrop.transform.position = Vector2.MoveTowards(rainDrop.transform.position, sunBeamPos, rainDropSpeed);
        }
        //else if(dist > 10 && moving)
        //{
        //    float pos = transform.position.x;
        //    if (!moveLeft)
        //    {
        //        pos -= 10f;
        //    }
        //    else
        //    {
        //       pos += 10f;
        //    }

        //    rainDrop.transform.position = new Vector2(pos, rainDrop.transform.position.y);



        //}

    }
}
