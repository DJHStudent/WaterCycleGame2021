using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    bool newSceneLoad = false;
    string nextLevel, currLevel;
    public Camera cam;
    public Text msgTxt;
    private void Start()
    {
        nextLevel = GameManager.levelStats.nextLevel;
        currLevel = GameManager.levelStats.currLevel;
        Time.timeScale = 0;
        switch (GameManager.trackingStats.currScene)
        {
            case 0:
                msgTxt.text = "";
                break;
            case 1:
                msgTxt.text = "1";
                break;
            case 2:
                msgTxt.text = "2";
                break;
            case 3:
                msgTxt.text = "3";
                break;
            case 4:
                msgTxt.text = "4";
                break;
            case 5:
                msgTxt.text = "5";
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x >= 0.99 && !newSceneLoad)
        {
            cam.enabled = true;
            SceneManager.UnloadSceneAsync(currLevel);
            SceneManager.LoadScene(nextLevel, LoadSceneMode.Additive);
            cam.enabled = false;
            newSceneLoad = true;
        }
        if(transform.localScale.x <= 0.01 && newSceneLoad)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("TransitionScene");
        }
    }
}
