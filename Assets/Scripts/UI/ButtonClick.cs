using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void onSceneChange(string scene)//when buttom clicked change scene to scene
    {
        SceneManager.LoadScene(scene);
    }
}
