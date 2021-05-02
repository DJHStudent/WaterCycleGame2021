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
                GameManager.levelUIManager.endLevel("The Raindrop Died :(");
            }
        }
        if (collision.gameObject.CompareTag("End"))//if clear the level
        {
            GameManager.levelUIManager.endLevel("Stage Cleared :)");
        }

        if (collision.gameObject.CompareTag("RainDrop"))//if collect a raindrop
        {
            GameManager.levelStats.updateScore(1);
            Destroy(collision.gameObject);
        }
    }
}
