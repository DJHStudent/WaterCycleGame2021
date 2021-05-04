using UnityEngine;

public class Collision : MonoBehaviour
{
    Animator playerAnim;
    private void Start()
    {
        playerAnim = GameManager.rainDrop.GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dead"))//if hit platform update trust and play animation
        {
            GameManager.levelStats.updateTrust(-50);
            playerAnim.SetTrigger("Flashing");
            //Debug.Log(GameManager.levelStats.playerTrust);
            if (GameManager.levelStats.playerTrust <= 0)//if no trust left die
            {
                GameManager.levelUIManager.onDeath("The Raindrop Left You");
            }
        }
        if (collision.gameObject.CompareTag("End"))//if clear the level
        {
            GameManager.levelUIManager.endLevel();
            //GameManager.levelUIManager.endLevel("Stage Cleared :)");
        }

        if (collision.gameObject.CompareTag("RainDrop"))//if collect a raindrop
        {
            GameManager.levelStats.updateScore(1);
            GameManager.levelStats.updateSize(0.5f);
            Destroy(collision.gameObject);
        }
    }
}
