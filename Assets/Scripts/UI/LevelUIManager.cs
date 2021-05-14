using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public Text deadTxt, scoreTxt, finishScoreTxt, finishTimeTxt, finishHeightTxt, stageTxt;
    public Text collectRaindropInfoTxt;
    public Button restartBtn;
    public Slider heightSlider, trustSlider;

    //Animator rainInfoAnim;
    [HideInInspector]public Animator collectRainAnim;
    public void endLevel()//save the stats from this level and transfer them to the next level once the transition has completed
    {
        onCompleteLvl();
        GameManager.levelStats.paused = true;
        GameManager.trackingStats.currTime += GameManager.levelStats.timeLevelLoaded;
        GameManager.trackingStats.currScore += GameManager.levelStats.score;
        GameManager.trackingStats.currHeight += Mathf.RoundToInt(GameManager.levelStats.timeLevelLoaded / 5);

        GameManager.trackingStats.currTrust = GameManager.levelStats.playerTrust;
        GameManager.trackingStats.currSize = GameManager.levelStats.playerSize;

        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
    }

    void onCompleteLvl()
    {
        switch (GameManager.trackingStats.currScene)
        {
            case 0:
                if (!GameManager.savedInfo.compTute)
                    GameManager.savedInfo.compTute = true; 
                break;
            case 1: 
                if(!GameManager.savedInfo.compLvl1)
                    GameManager.savedInfo.compLvl1 = true; 
                break;
            case 2:
                if (!GameManager.savedInfo.compLvl2)
                    GameManager.savedInfo.compLvl2 = true; 
                break;
            case 3:
                if (!GameManager.savedInfo.compLvl3)
                    GameManager.savedInfo.compLvl3 = true; 
                break;
            case 4:
                if (!GameManager.savedInfo.compLvl4)
                    GameManager.savedInfo.compLvl4 = true; 
                break;
            case 5:
                if (!GameManager.savedInfo.compLvl5)
                    GameManager.savedInfo.compLvl5 = true; 
                break;
            default: break;
        }
    }

    public void onDeath(string message)
    {
        GameManager.levelStats.notrust = true;
        GameManager.levelStats.paused = true;
        Time.timeScale = 1;
        GameManager.levelStats.saveBestStats();

        deadTxt.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
        finishScoreTxt.gameObject.SetActive(true);
        finishTimeTxt.gameObject.SetActive(true);
        finishHeightTxt.gameObject.SetActive(true);

        deadTxt.text = message;
        finishScoreTxt.text = "Score: " + GameManager.levelStats.score * 100;
        float totalTime = GameManager.levelStats.timeLevelLoaded + GameManager.trackingStats.currTime;
        GameManager.trackingStats.currTime += GameManager.levelStats.timeLevelLoaded;
        int mins = Mathf.FloorToInt(totalTime / 60);
        float secs = Mathf.Floor(totalTime % 60);
        finishTimeTxt.text = "Time: " + mins.ToString("00") + ":" + secs.ToString("00"); //note kinda inefficent here as off by a little based on frame inaccuracyies
        finishHeightTxt.text = "Height: " + (GameManager.trackingStats.currHeight + Mathf.RoundToInt(GameManager.levelStats.timeLevelLoaded / 5)) + "m";
    }
    public void onCollectRaindrop() //play the fade animation for this text
    {
        if (collectRainAnim.speed == 0) //if never played before activate the gameObject
        {
            GameManager.levelUIManager.collectRaindropInfoTxt.gameObject.SetActive(true);
            collectRainAnim.speed = 1;
        }
        collectRainAnim.Play("collectInfoTxt", -1, 0);//SetTrigger("Fade");
    }

    public void updateHeight(float time)
    {
        heightSlider.value = time;
    }

    public void maxHeight(float max)//set the max value for the slider
    {
        heightSlider.maxValue = max;
    }
    public void setTrust(float value)
    {
        trustSlider.value = value;
    }

    public void updateScore()
    {
        scoreTxt.text = "Score: " + GameManager.levelStats.score * 100;
    }
}
