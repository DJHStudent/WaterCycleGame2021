using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunBeamPreMove : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector2 endPos;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        endPos.y = -50;
        endPos.x = 25;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(1, Vector2.MoveTowards(lineRenderer.GetPosition(1), endPos, Time.deltaTime * 8));
        if(Vector2.Distance(endPos, lineRenderer.GetPosition(1)) < 0.1)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
