using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject rainDrop;
    void Awake()
    {
        rainDrop = GameObject.Find("RainDrop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
