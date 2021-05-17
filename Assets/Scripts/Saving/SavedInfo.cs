using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedInfo : MonoBehaviour
{
    public int highScore = 0, maxHight = 0, backVolume = 100, soundEfxVolume = 100;
    public bool compTute = true, compLvl1 = false, compLvl2 = false, compLvl3 = false, compLvl4 = false, compLvl5 = false;

    const string saveHeight = "height";
    const string saveScore = "score";
    const string savedback = "back";
    const string saveSoundEfx = "soundEfx";
    const string saveLvl1 = "lvl1";
    const string saveLvl2 = "lvl2";
    const string saveLvl3 = "lvl3";
    const string saveLvl4 = "lvl4";
    const string saveLvl5 = "lvl5";

    public AudioSource backAudio, soundEfxAudio;
    public AudioClip damageClip, deadClip, collectClip;
    private void Start()
    {
        loadSave();
    }

    void loadSave() //if the info has been saved before load it in
    {
        if (PlayerPrefs.HasKey(saveHeight))
            maxHight = PlayerPrefs.GetInt(saveHeight);
        if (PlayerPrefs.HasKey(saveScore))
            highScore = PlayerPrefs.GetInt(saveScore);
        if (PlayerPrefs.HasKey(savedback))
        {
            backVolume = PlayerPrefs.GetInt(savedback);
            backAudio.volume = (float)backVolume / 100;
        }
        if (PlayerPrefs.HasKey(saveSoundEfx))
        {
            soundEfxVolume = PlayerPrefs.GetInt(saveSoundEfx);
            soundEfxAudio.volume = (float)soundEfxVolume / 100;
        }

        if (PlayerPrefs.HasKey(saveLvl1))
            compLvl1 = intToBool(PlayerPrefs.GetInt(saveLvl1));
        if (PlayerPrefs.HasKey(saveLvl2))
            compLvl2 = intToBool(PlayerPrefs.GetInt(saveLvl2));
        if (PlayerPrefs.HasKey(saveLvl3))
            compLvl3 = intToBool(PlayerPrefs.GetInt(saveLvl3));
        if (PlayerPrefs.HasKey(saveLvl4))
            compLvl4 = intToBool(PlayerPrefs.GetInt(saveLvl4));
        if (PlayerPrefs.HasKey(saveLvl5))
            compLvl5 = intToBool(PlayerPrefs.GetInt(saveLvl5));
    }

    public void saveInfo()
    {
        if (!PlayerPrefs.HasKey(saveHeight) || maxHight > PlayerPrefs.GetInt(saveHeight))
            PlayerPrefs.SetInt(saveHeight, maxHight);
        if (!PlayerPrefs.HasKey(saveScore) || highScore > PlayerPrefs.GetInt(saveScore))
            PlayerPrefs.SetInt(saveScore, highScore);

        saveLvlOn();
    }

    public void saveBackVolum()
    {
        if (!PlayerPrefs.HasKey(savedback) || backVolume != PlayerPrefs.GetInt(savedback))
        {
            PlayerPrefs.SetInt(savedback, backVolume);
        }
    }

    public void savedSoundEfxVolum()
    {
        if (!PlayerPrefs.HasKey(saveSoundEfx) || soundEfxVolume != PlayerPrefs.GetInt(saveSoundEfx))
            PlayerPrefs.SetInt(saveSoundEfx, soundEfxVolume);
    }

    public void saveLvlOn()
    {
        if (!PlayerPrefs.HasKey(saveLvl1) || compLvl1 != intToBool(PlayerPrefs.GetInt(saveLvl1)))
            PlayerPrefs.SetInt(saveLvl1, boolToInt(compLvl1));
        if (!PlayerPrefs.HasKey(saveLvl2) || compLvl2 != intToBool(PlayerPrefs.GetInt(saveLvl2)))
            PlayerPrefs.SetInt(saveLvl2, boolToInt(compLvl2));
        if (!PlayerPrefs.HasKey(saveLvl3) || compLvl3 != intToBool(PlayerPrefs.GetInt(saveLvl3)))
            PlayerPrefs.SetInt(saveLvl3, boolToInt(compLvl3));
        if (!PlayerPrefs.HasKey(saveLvl4) || compLvl4 != intToBool(PlayerPrefs.GetInt(saveLvl4)))
            PlayerPrefs.SetInt(saveLvl4, boolToInt(compLvl4));
        if (!PlayerPrefs.HasKey(saveLvl5) || compLvl5 != intToBool(PlayerPrefs.GetInt(saveLvl5)))
            PlayerPrefs.SetInt(saveLvl5, boolToInt(compLvl5));
    }

    bool intToBool(int value)
    {
        return value == 1; //1 return true, 0 return false
    }

    int boolToInt(bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }

    void OnApplicationQuit()
    {
        saveInfo();
        saveBackVolum();
        savedSoundEfxVolum();
    }
}
