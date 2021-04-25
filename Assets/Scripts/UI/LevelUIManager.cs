using UnityEngine.UI;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public Text deadTxt, healthTxt;
    public Button restartBtn;
    public Slider heightSlider;


    public void updateHeight(float time)
    {
        heightSlider.value = time;
    }

    public void maxHeight(float max)
    {
        heightSlider.maxValue = max;
    }

    public void updateScore()
    {
        healthTxt.text = "Score: " + GameManager.levelStats.score;
    }
}
