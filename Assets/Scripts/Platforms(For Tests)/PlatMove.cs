using UnityEngine;

public class PlatMove : MonoBehaviour
{
    float speed = 20;
    float destroyYPos = -54;

    void Update()
    {
        if (!GameManager.levelStats.paused) //move at the specified speed until reach level bottom
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= destroyYPos && this.gameObject != GameManager.levelGen.newDist.gameObject)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
