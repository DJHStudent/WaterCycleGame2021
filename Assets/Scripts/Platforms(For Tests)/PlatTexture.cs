using UnityEngine;

public class PlatTexture : MonoBehaviour
{
    public Sprite[] texture;//determine a random texture to apply to the given object from the array
    void Start()
    {
        int rand = Random.Range(0, texture.Length);
        GetComponent<SpriteRenderer>().sprite = texture[rand];
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }
}
