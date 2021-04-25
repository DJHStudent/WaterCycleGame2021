using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.levelUIManager.deadTxt.gameObject.SetActive(true);
            GameManager.levelUIManager.restartBtn.gameObject.SetActive(true);
            GameManager.levelStats.paused = true;
            Time.timeScale = 1;
        }

        if (collision.gameObject.CompareTag("RainDrop"))
        {
            GameManager.levelStats.updateScore(1);
            Destroy(collision.gameObject);
        }
    }
}
