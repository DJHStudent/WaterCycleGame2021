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
        SceneManager.LoadScene(scene);
    }

    public void back(string scene)
    {
        GameManager.trackingStats.resetStats();
        SceneManager.LoadScene(scene);
    }

    public void setLevel(int levelNum)
    {
        GameObject.Find("SaveManager").GetComponent<TrackingStats>().currScene = levelNum;
        Debug.Log(GameObject.Find("SaveManager").GetComponent<TrackingStats>().currScene);
    }
}
