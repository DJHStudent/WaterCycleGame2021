using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsChange : MonoBehaviour
{
    SavedInfo savedInfo;

    public Slider backSlider, soundefxSlider;
    public AudioSource backAudio, soundfxAudio;
    public Text backTxt, soundEfxTxt, scoreTxt, heightTxt;
    private void Start()
    {
        savedInfo = GameObject.Find("SaveManager").GetComponent<SavedInfo>();
        backAudio = GameObject.Find("SaveManager").GetComponent<AudioSource>();

        backSlider.value = savedInfo.backVolume;
        backTxt.text = "" + (int)backSlider.value;

        //backSlider.value = savedInfo.soundEfxVolume * 100;
        //backTxt.text = "" + (int)backSlider.value;

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
        backAudio.volume = backSlider.value / 100;
        savedInfo.backVolume = (int)backSlider.value;
        //savedInfo.saveBackVolum();
    }

    public void adjustSoundEfxVolume()
    {
        soundEfxTxt.text = "" + (int)soundefxSlider.value;
        savedInfo.soundEfxVolume = (int)soundefxSlider.value;
        //savedInfo.savedSoundEfxVolum();
    }






    public void unlockAll()
    {
        savedInfo.compTute = true;
        savedInfo.compLvl1 = true;
        savedInfo.compLvl2 = true;
        savedInfo.compLvl3 = true;
        savedInfo.compLvl4 = true;
        savedInfo.compLvl5 = true;

        //savedInfo.saveLvlOn();
    }
}
