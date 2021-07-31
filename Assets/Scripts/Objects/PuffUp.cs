using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffUp : MonoBehaviour
{
    public LayerMask layer;
    float maxDist = 20;
    bool isPuffed = false;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    //if at any point sunbeam on it for x seconds it panics and runs away

    // Update is called once per frame
    void Update()
    {
        inRange();
    }
    //state machine!!!
    void inRange()
    {
        if(Physics2D.OverlapCircle(transform.position, maxDist, layer) && !isPuffed )
        {
            //puff up anim, fast enough that will always complete but slow enough to actually be noticed and avoided
            //puff up, wait some time and depuff if player no longer in range
            anim.SetTrigger("DoPuff");
            isPuffed = true;
        }
        else if(isPuffed)
        {
       //     anim.SetTrigger("DoPuff");
            //shrink anim back to normal size
        }

    }
}
