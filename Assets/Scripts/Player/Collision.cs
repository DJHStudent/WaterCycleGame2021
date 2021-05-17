using UnityEngine;

public class Collision : MonoBehaviour
{
    Animator playerAnim, camAnim;
    public bool takenDamage = false;
    private void Start()
    {
        playerAnim = GameManager.rainDrop.GetComponent<Animator>();
        camAnim = Camera.main.GetComponent<Animator>();
        //Debug.Log(camAnim);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dead") && !takenDamage)//if hit platform update trust and play animation
        {
            GameManager.levelStats.updateTrust(-50);
            playerAnim.SetTrigger("Flashing");
            camAnim.SetTrigger("Shake");
            takenDamage = true;
            Invoke("damage", .45f);
            //Debug.Log(GameManager.levelStats.playerTrust);
            if (GameManager.levelStats.playerTrust <= 0)//if no trust left die
            {
                Destroy(gameObject.GetComponent<PolygonCollider2D>());
                GameManager.levelUIManager.onDeath("The Raindrop Left You");
            }
        }
        if (collision.gameObject.CompareTag("End"))//if clear the level
        {
            //Debug.Log("Height: " + (GameManager.trackingStats.currHeight + Mathf.RoundToInt(GameManager.levelStats.timeLevelLoaded / 5)) + "m");
            GameManager.levelUIManager.endLevel();
        }

        if (collision.gameObject.CompareTag("RainDrop"))//if collect a raindrop
        {
            GameManager.levelStats.updateScore(1);
            GameManager.levelStats.updateSize(2);
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<TrailRenderer>().enabled = false;
            collision.gameObject.GetComponent<ParticleSystem>().Play();
            if (GameManager.trackingStats.currScene == 0)
            {
                GameManager.levelUIManager.onCollectRaindrop();
            }
            //collision.gameObject.transform.GetChild(0).parent = null;
            //Destroy(collision.gameObject);
        }
    }
    void damage()
    {
        takenDamage = false;
    }
}
