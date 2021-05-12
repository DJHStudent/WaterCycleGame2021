using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{

    float maxSpeed = 50;
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
        if (!GameManager.levelStats.paused || GameManager.levelStats.tutActive)
        {
            moveSunBeam();
            moveRainDrop();
        }
        if (GameManager.levelStats.notrust)
            noTrust();
    }

    void moveSunBeam() //update the sunbeams position based on input given and ensure never go out of level bounds
    {
        Vector3 pos = transform.position;
        //set the amount to move the object by
        float xSpeed = Input.GetAxis("Horizontal"); float ySpeed = 0;
        if (GameManager.trackingStats.currScene >= 2)//only allow moving up if on level 2 or greater
            ySpeed = Input.GetAxis("Vertical");
        Vector3 movePos = new Vector3(xSpeed, ySpeed);
        movePos = Vector3.ClampMagnitude(movePos, 1);//so not faster on the diagonal
        
        //update the objects position
        pos += movePos * maxSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -28.3f, 28.3f); //ensures never goes off screen
        pos.y = Mathf.Clamp(pos.y, -48.0f, 46.0f);
        transform.position = pos;

        //update the sunbeam's visuals
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
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moving = true;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moving = true;
        }
        

    }

    void moveRainDrop()//get the raindrop to follow the sunbeam
    {
        Vector2 rainDropPos = GameManager.rainDrop.transform.position;
        float dist = Vector2.Distance(transform.position, rainDropPos);//get dist from one to the other

        if (dist > 0.01)//if not too close to the sunbeam already
        {
            float trustReduction = Mathf.Pow(GameManager.levelStats.playerTrust / 100, 0.5f);//.65 //how reduced the speed becomes based on the players trust
            GameManager.rainDrop.transform.position = Vector2.MoveTowards(rainDropPos, transform.position, maxSpeed * Time.deltaTime * trustReduction); //move towards new position with current speed
        }
        updateTrail();

        rainLookAt();
    }
    void rainLookAt()//get the angle of the sunbeam so looks in direction moving
    {
        float dir = GameManager.rainDrop.transform.position.x - transform.position.x;
        GameManager.rainDrop.transform.rotation = Quaternion.Euler(0,0,180+dir*2);
    }

    void noTrust()
    {
        GameManager.rainDrop.transform.rotation = Quaternion.identity;
        Vector2 pos = GameManager.rainDrop.transform.position;
        pos.y -= 15 * Time.deltaTime;
        GameManager.rainDrop.transform.position = pos;

        updateTrail();
    }

    void updateTrail()
    {
        rainLineRenderer.SetPosition(0, GameManager.rainDrop.transform.position);
        rainLineRenderer.SetPosition(1, GameManager.rainDrop.transform.position + GameManager.rainDrop.transform.up * (10 + GameManager.levelStats.speed * 2));
    }
}
