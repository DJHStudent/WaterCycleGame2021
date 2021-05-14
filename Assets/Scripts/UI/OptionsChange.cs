using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsChange : MonoBehaviour
{
    SavedInfo savedInfo;
    private void Start()
    {
        savedInfo = GameObject.Find("SaveManager").GetComponent<SavedInfo>();
    }

    public void unlockAll()
    {
        savedInfo.compTute = true;
        savedInfo.compLvl1 = true;
        savedInfo.compLvl2 = true;
        savedInfo.compLvl3 = true;
        savedInfo.compLvl4 = true;
        savedInfo.compLvl5 = true;
    }
}
