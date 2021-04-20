using UnityEngine.UI;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public Text deadTxt, healthTxt;
    public Button restartBtn;

    public void updateHealth()
    {
        healthTxt.text = "Health: " + GameManager.movementManager.health;
    }
}
