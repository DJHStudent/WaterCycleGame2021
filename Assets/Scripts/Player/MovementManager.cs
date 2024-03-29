using System;
using UnityEngine;


public class MovementManager : MonoBehaviour
{
    Animator anim, keyTxtAnim;
    float maxSpeed = 50;
    LineRenderer sunLineRenderer, rainLineRenderer;
    Collision collision;

    bool disolveKeyTxt = false;

    public static bool pauseBegining = false;
    private void Start()
    {
        if (pauseBegining)
        {
            GameManager.levelStats.paused = true;
            pauseBegining = false;
        }
        collision = GameManager.rainDrop.GetComponent<Collision>();
        sunLineRenderer = GetComponent<LineRenderer>();
        rainLineRenderer = GameManager.rainDrop.GetComponent<LineRenderer>();
        anim = GameManager.rainDrop.GetComponent<Animator>();

        if(GameManager.trackingStats.currScene == 2)
        {
            keyTxtAnim = GameObject.Find("keyTxt").GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.levelStats.paused || GameManager.levelStats.tutActive)
        {
            moveSunBeam();
            moveRainDrop();

            rainDropAnim();
            disolveTxt();
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
    }

    void moveRainDrop()//get the raindrop to follow the sunbeam
    {
        Vector2 rainDropPos = GameManager.rainDrop.transform.position;
        float dist = Vector2.Distance(transform.position, rainDropPos);//get dist from one to the other

        if (dist > 0.01)//if not too close to the sunbeam already
        {
            //-1 + Mathf.Pow(1.6f, (GameManager.levelStats.playerTrust / 100) * 1.5f);
            float trustReduction = Mathf.Pow(GameManager.levelStats.playerTrust / 100, 0.6f);//.65 //how reduced the speed becomes based on the players trust
            trustReduction = Mathf.Clamp(trustReduction, 0.27f, 1); //so never gets too slow
            GameManager.rainDrop.transform.position = Vector2.MoveTowards(rainDropPos, transform.position, maxSpeed * Time.deltaTime * trustReduction); //move towards new position with current speed


        }
        updateTrail();

        rainLookAt();
    }
    void rainLookAt()//get the angle of the sunbeam so looks in direction moving
    {
        float dir = GameManager.rainDrop.transform.position.x - transform.position.x;
        GameManager.rainDrop.transform.rotation = Quaternion.Euler(0,0,dir*2);
    }

    void noTrust() //move raindop as falling down away from the sun
    {
        GameManager.rainDrop.transform.rotation = Quaternion.Euler(0,0, 180);
        Vector2 pos = GameManager.rainDrop.transform.position;
        pos.y -= 15 * Time.deltaTime;
        GameManager.rainDrop.transform.position = pos;

        updateTrail();
    }

    void updateTrail()
    {
        rainLineRenderer.SetPosition(0, GameManager.rainDrop.transform.position);
        rainLineRenderer.SetPosition(1, GameManager.rainDrop.transform.position - GameManager.rainDrop.transform.up * (10 + GameManager.levelStats.speed * 2));
    }


    void disolveTxt()
    {
        if (!disolveKeyTxt && GameManager.trackingStats.currScene == 2)
        {
            if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                disolveKeyTxt = true;
                keyTxtAnim.enabled = true;
            }
        }
    }
    
    void rainDropAnim()
    {
        if (!collision.takenDamage) //only adjust if not currently flashing
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                anim.SetTrigger("isLeft");
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                anim.SetTrigger("isIdle");
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                anim.SetTrigger("isRight");
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                anim.SetTrigger("isIdle");
            }
            if (GameManager.trackingStats.currScene >= 2)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    anim.SetTrigger("isSquish");
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
                {
                    anim.SetTrigger("isIdle");
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    anim.SetTrigger("isStretch");
                }
                else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
                {
                    anim.SetTrigger("isIdle");
                }
            }
        }
    }
}
