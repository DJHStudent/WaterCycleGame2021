using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    bool newSceneLoad = false;
    string nextLevel, currLevel;
    public Camera cam;

    private void Start()
    {
        nextLevel = GameManager.levelStats.nextLevel;
        currLevel = GameManager.levelStats.currLevel;
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 0 && !newSceneLoad)
        {
            cam.enabled = true;
            SceneManager.UnloadSceneAsync(currLevel);
            SceneManager.LoadScene(nextLevel, LoadSceneMode.Additive);
            cam.enabled = false;
            newSceneLoad = true;
        }
        if(transform.position.x < -79.8f)
        {
            SceneManager.UnloadSceneAsync("TransitionScene");
        }
    }
}
