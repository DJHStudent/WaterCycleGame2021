using UnityEngine.UI;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public Text deadTxt, healthTxt, finishScoreTxt, finishTimeTxt, finishHeightTxt;
    public Button restartBtn;
    public Slider heightSlider, trustSlider;

    public void endLevel(string message)
    {
        GameManager.levelStats.paused = true;
        Time.timeScale = 1;

        deadTxt.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
        finishScoreTxt.gameObject.SetActive(true);
        finishTimeTxt.gameObject.SetActive(true);
        finishHeightTxt.gameObject.SetActive(true);

        deadTxt.text = message;
        finishScoreTxt.text = "Score: " + GameManager.levelStats.score;
        int mins = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60);
        float secs = Mathf.Floor(Time.timeSinceLevelLoad % 60);
        finishHeightTxt.text = "Time: " + mins.ToString("00") + ":" + secs.ToString("00"); //note kinda inefficent here as off by a little based on frame inaccuracyies
        finishTimeTxt.text = "Height: " + Mathf.Round(GameManager.levelStats.timeLevelLoaded / 5) + "m";
    }
    public void updateHeight(float time)
    {
        heightSlider.value = time;
    }

    public void maxHeight(float max)
    {
        heightSlider.maxValue = max;
    }
    public void setTrust(float value)
    {
        trustSlider.value = value;
    }

    public void updateScore()
    {
        healthTxt.text = "Score: " + GameManager.levelStats.score;
    }
}
