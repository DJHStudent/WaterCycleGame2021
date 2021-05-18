using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsChange : MonoBehaviour
{
    SavedInfo savedInfo;

    public Slider backSlider, soundefxSlider;
    public Text backTxt, soundEfxTxt, scoreTxt, heightTxt;
    private void Start()
    {
        savedInfo = GameObject.Find("SaveManager").GetComponent<SavedInfo>();

        backSlider.value = savedInfo.backVolume;
        backTxt.text = "" + (int)backSlider.value;

        soundefxSlider.value = savedInfo.soundEfxVolume;
        soundEfxTxt.text = "" + (int)soundefxSlider.value;

        setHighScore();
    }

    void setHighScore()
    {
        scoreTxt.text = "Score: " + savedInfo.highScore;
        heightTxt.text = "Height: " + savedInfo.maxHight + "m";
    }

    public void adjustBackVolume()
    {
        backTxt.text = "" + (int)backSlider.value;
        savedInfo.backAudio.volume = backSlider.value / 100;
        savedInfo.backVolume = (int)backSlider.value;
    }

    public void adjustSoundEfxVolume()
    {
        soundEfxTxt.text = "" + (int)soundefxSlider.value;
        savedInfo.soundEfxAudio.volume = soundefxSlider.value / 100;
        savedInfo.soundEfxVolume = (int)soundefxSlider.value;

        savedInfo.soundEfxAudio.clip = savedInfo.damageClip;
        savedInfo.soundEfxAudio.Play();
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
