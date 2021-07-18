using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffUp : MonoBehaviour
{
    float maxDist = 5;
    bool isPuffed = false;
    void Start()
    {
        
    }
    //if at any point sunbeam on it for x seconds it panics and runs away

    // Update is called once per frame
    void Update()
    {
        
    }
    //state machine!!!
    void inRange()
    {
        if(Physics2D.OverlapCircle(transform.position, maxDist, 6))
        {
            //puff up anim, fast enough that will always complete but slow enough to actually be noticed and avoided
            //puff up, wait some time and depuff if player no longer in range
            isPuffed = true;
        }
        else if(isPuffed)
        {
            //shrink anim back to normal size
        }

    }
}
