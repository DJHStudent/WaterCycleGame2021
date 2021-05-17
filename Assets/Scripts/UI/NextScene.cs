using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextScene : MonoBehaviour
{
    bool newSceneLoad = false;
    string nextLevel, currLevel;
    public Camera cam;

    private void Start()
    {
        nextLevel = GameManager.levelStats.nextLevel;
        currLevel = GameManager.levelStats.currLevel;
        Time.timeScale = 0;
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
