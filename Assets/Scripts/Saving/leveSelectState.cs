using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leveSelectState : MonoBehaviour
{
    public Button tuteBtn, lvl1Btn, lvl2Btn, lvl3Btn, lvl4Btn, lvl5Btn;
    SavedInfo savedInfo;
    void Start()
    {
        savedInfo = GameObject.Find("SaveManager").GetComponent<SavedInfo>();
        setActive();
    }

    void setActive()
    {
        if (savedInfo.compTute)
            tuteBtn.interactable = true;
        if (savedInfo.compLvl1)
            lvl1Btn.interactable = true;
        if (savedInfo.compLvl2)
            lvl2Btn.interactable = true;
        if (savedInfo.compLvl3)
            lvl3Btn.interactable = true;
        if (savedInfo.compLvl4)
            lvl4Btn.interactable = true;
        if (savedInfo.compLvl5)
            lvl5Btn.interactable = true;
    }
}
