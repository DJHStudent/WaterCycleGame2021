using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedInfo : MonoBehaviour
{
    public int highScore = 0, maxHight = 0, backVolume = 100, soundEfxVolume = 100;
    public bool compTute = true, compLvl1 = false, compLvl2 = false, compLvl3 = false, compLvl4 = false, compLvl5 = false;

    private void Start()
    {
        //actually load in the info saved elsewhere
    }
}
