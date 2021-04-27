using UnityEngine;

public class PlatTexture : MonoBehaviour
{
    public Sprite[] texture;
    void Start()
    {
        int rand = Random.Range(0, texture.Length);
        GetComponent<SpriteRenderer>().sprite = texture[rand];
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }
}
