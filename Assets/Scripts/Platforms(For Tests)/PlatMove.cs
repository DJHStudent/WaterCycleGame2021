using UnityEngine;

public class PlatMove : MonoBehaviour
{
    //note this script just for testing purposes on 20/04/2021
    float speed = 20;
    float destroyYPos = -54;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
            if (transform.position.y <= destroyYPos)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
