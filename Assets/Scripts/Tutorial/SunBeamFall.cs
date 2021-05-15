using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class SunBeamFall : MonoBehaviour
{
    LineRenderer lineRenderer;
    MovementManager movementManager;
    Vector2 endPos;
    public Text msgTxt, keyTxt;

    bool lineBottom = false, moveLearn = false, collectRainLearn = false;
    public Animator trustBar;
    RainSpawn rainSpawn;
    void Start()
    {
        GameManager.levelUIManager.collectRainAnim = GameManager.levelUIManager.collectRaindropInfoTxt.gameObject.GetComponent<Animator>();
        GameManager.levelUIManager.collectRainAnim.speed = 0;
        GameManager.levelUIManager.collectRaindropInfoTxt.gameObject.SetActive(false);

        GameManager.levelStats.paused = true;
        GameManager.levelStats.tutActive = true;
        lineRenderer = GetComponent<LineRenderer>();
        movementManager = GetComponent<MovementManager>();
        endPos.y = -35;
        rainSpawn = GameObject.Find("SceneManager").GetComponent<RainSpawn>();
        //Instantiate(rainSpawn.raindrop, new Vector2(0, 54), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lineBottom)
        {
            if (lineRenderer.GetPosition(1).y > -35)
            {
                lineRenderer.SetPosition(1, Vector2.MoveTowards(lineRenderer.GetPosition(1), endPos, Time.deltaTime * 20));
            }
            else
            {
                lineBottom = true;
                movementManager.enabled = true;
                GameManager.levelStats.enabled = true;
                showCaseText();
            }
        }
        else if (Input.GetAxis("Horizontal") != 0 && !moveLearn)
        {
            GameManager.levelStats.updateTrust(Time.deltaTime * 20);
        }
        if (GameManager.levelStats.playerTrust >= 100 && !moveLearn)
        {
            keyTxt.gameObject.SetActive(false);
            msgTxt.text = "Hurry Collect\nfalling raindrops\nor the raindrop \nwill evaporate";
            trustBar.SetTrigger("StopFlash");
            moveLearn = true;
            collectRainLearn = true;
            rainSpawn.tuteSpawn = true;
            //start raindrops spawning
        }
        else if (collectRainLearn)
        {
            GameManager.levelStats.updateSize(-Time.deltaTime * 0.25f); //slowly loose mass over time
        }

        if(GameManager.rainDrop.transform.localScale.x >= 7.9f)
        {
            msgTxt.text = "Watch Out and\nKeep the Raindrop safe";
            GameManager.levelStats.tutActive = false;
            collectRainLearn = false;
            GameManager.levelGen.enabled = true;
            GameManager.levelGen.initialiseGeneration(); //start generating in platforms
            GameManager.levelStats.paused = false;
        }
    }

    void showCaseText()
    {
        msgTxt.gameObject.SetActive(true);
        keyTxt.gameObject.SetActive(true);
        trustBar.SetTrigger("Flash");
    }
}
