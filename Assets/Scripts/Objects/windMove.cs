using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class windMove : MonoBehaviour
{
    float maxCheckDist = 200;
    Vector2 dir;
    bool move = true;

    public GameObject wind1, wind2, wind3;
    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        startWind();
        Invoke("wait", 15);
    }
    void wait()
    {
        move = false;
    }

    public void startWind()
    {
        //spriteRenderer.enabled = true;
        if (Random.value >= 0.5)
        {
            dir = new Vector2(-1, 0);
            transform.localScale = new Vector2(-1, 1);
            transform.position = new Vector2(15, 0);
        }
        else {
            dir = new Vector2(1, 0);
            transform.localScale = new Vector2(1, 1);
            transform.position = new Vector2(-15, 0);
        }
        wind1.SetActive(true);
        wind2.SetActive(true);
        wind3.SetActive(true);
        move = true;
        Invoke("stopWind", Random.Range(20, 30));
    }

    public void stopWind()
    {
        wind1.SetActive(false);
        wind2.SetActive(false);
        wind3.SetActive(false);
        move = false;
    }

    private void Update()
    {
        if (move && !GameManager.levelStats.paused)
        {
            checkNearby();
        }
    }

    void checkNearby()
    {
        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, maxCheckDist);
        foreach (Collider2D coll in collision)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.transform.Translate(dir * 30 * Time.deltaTime, Space.World);
                coll.gameObject.transform.position = new Vector2(Mathf.Clamp(coll.gameObject.transform.position.x, -28.3f, 28.3f), coll.gameObject.transform.position.y);
            }
            else
                coll.gameObject.transform.Translate(dir * 5 * Time.deltaTime, Space.World);
        }
    }
}
