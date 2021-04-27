using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.levelStats.updateTrust(-50);
            //Debug.Log(GameManager.levelStats.playerTrust);
            if (GameManager.levelStats.playerTrust <= 0)
            {
                GameManager.levelUIManager.endLevel("The Raindrop Died :(");
            }
        }
        if (collision.gameObject.CompareTag("End"))
        {
            GameManager.levelUIManager.endLevel("Stage Cleared :)");
        }

        if (collision.gameObject.CompareTag("RainDrop"))
        {
            GameManager.levelStats.updateScore(1);
            Destroy(collision.gameObject);
        }
    }
}
