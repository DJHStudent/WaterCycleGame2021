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
    public GameObject continueBtn;
    Animator anim;
    private void Start()
    {
        nextLevel = GameManager.levelStats.nextLevel;
        currLevel = GameManager.levelStats.currLevel;
        GameManager.levelStats.paused = true;
        Time.timeScale = 0;
        anim = GetComponent<Animator>();
        switch (GameManager.trackingStats.currScene)
        {
            case 0:
                msgTxt.text = "";
                break;
            case 1:
                msgTxt.text = "Getting a burst of enery as it leaves the water, it suddenly sees a new danger...";
                break;
            case 2:
                msgTxt.text = "The light of its lover seemed to grow brighter, giving it a power it had not felt before...";
                break;
            case 3:
                msgTxt.text = "Braving further dangers the raindrop continued skyward, feeling a new warmth...";
                break;
            case 4:
                msgTxt.text = "Nothing can stand in our way, the raindrop thinks, as sudden tugging begins pushing on the raindrop...";
                break;
            case 5:
                msgTxt.text = "The final frontier, the raindrop can feel the embrace of its lover growing ever nearer...";
                break;
        }
        msgTxt.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x >= 1 && !newSceneLoad)
        {
            msgTxt.enabled = true;
            continueBtn.SetActive(true);
            transform.localScale = new Vector2(1, 1);
        }
        if(transform.localScale.x <= 0.01 && newSceneLoad)
        {
            Time.timeScale = 1;
            GameManager.levelStats.paused = false;
            SceneManager.UnloadSceneAsync("TransitionScene");
        }
    }

    public void buttonClick()
    {
        msgTxt.enabled = false;
        continueBtn.SetActive(false);
        //cam.enabled = true;
        SceneManager.UnloadSceneAsync(currLevel);
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Additive);
        GameManager.levelStats.paused = true;
        Time.timeScale = 0;
        cam.enabled = false;
        newSceneLoad = true;
        anim.SetTrigger("GoBack");
    }
}
