using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void onSceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
