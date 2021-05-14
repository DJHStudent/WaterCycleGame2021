using UnityEngine;

public class Collision : MonoBehaviour
{
    Animator playerAnim, camAnim;
    private void Start()
    {
        playerAnim = GameManager.rainDrop.GetComponent<Animator>();
        camAnim = Camera.main.GetComponent<Animator>();
        //Debug.Log(camAnim);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dead"))//if hit platform update trust and play animation
        {
            GameManager.levelStats.updateTrust(-50);
            playerAnim.SetTrigger("Flashing");
            camAnim.SetTrigger("Shake");
            //Debug.Log(GameManager.levelStats.playerTrust);
            if (GameManager.levelStats.playerTrust <= 0)//if no trust left die
            {
                Destroy(gameObject.GetComponent<PolygonCollider2D>());
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
            GameManager.levelStats.updateSize(2);
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<TrailRenderer>().enabled = false;
            collision.gameObject.GetComponent<ParticleSystem>().Play();
            //collision.gameObject.transform.GetChild(0).parent = null;
            //Destroy(collision.gameObject);
        }
    }
}
