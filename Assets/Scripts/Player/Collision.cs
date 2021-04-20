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
            GameManager.paused = true;
        }  
        
        if (collision.gameObject.CompareTag("RainDrop"))
        {
            GameManager.movementManager.updateHealth(1);
            Destroy(collision.gameObject);
        }
    }
}
