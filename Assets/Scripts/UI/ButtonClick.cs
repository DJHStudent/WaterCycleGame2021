using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void onSceneChange(string scene)//when buttom clicked change scene to scene
    {
        SceneManager.LoadScene(scene);
    }

    public void startLevel(string scene)
    {
        GameObject.Find("SaveManager").GetComponent<TrackingStats>().loadingScene = scene;
        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
    }

    public void back()
    {
        GameManager.levelStats.saveBestStats();
        GameManager.trackingStats.resetStats();
        GameManager.savedInfo.saveInfo();
        SceneManager.LoadScene("MainMenu");
    }

    public void setLevel(int levelNum)
    {
        GameObject.Find("SaveManager").GetComponent<TrackingStats>().currScene = levelNum;
    }
    public void setLevelHeight(int height)
    {
        GameObject.Find("SaveManager").GetComponent<TrackingStats>().currHeight = height;
    }
}