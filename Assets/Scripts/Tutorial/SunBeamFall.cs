using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBeamFall : MonoBehaviour
{
    LineRenderer lineRenderer;
    MovementManager movementManager;
    Vector2 endPos;
    public Text msgTxt, keyTxt;

    bool lineBottom = false, moveLearn = false;
    public Animator trustBar;
    void Start()
    {
        GameManager.levelStats.paused = true;
        GameManager.levelStats.tutActive = true;
        lineRenderer = GetComponent<LineRenderer>();
        movementManager = GetComponent<MovementManager>();
        endPos.y = -35;
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
            msgTxt.text = "Watch Out and\nKeep the Raindrop safe";
            trustBar.SetTrigger("StopFlash");
            moveLearn = true;
            GameManager.levelGen.enabled = true;
            GameManager.levelStats.paused = false;
            GameManager.levelStats.tutActive = false;
        }
    }

    void showCaseText()
    {
        msgTxt.gameObject.SetActive(true);
        keyTxt.gameObject.SetActive(true);
        trustBar.SetTrigger("Flash");
    }
}
