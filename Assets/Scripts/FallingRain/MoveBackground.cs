using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    Material backgroundMat;
    void Start()
    {
        backgroundMat = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMat.mainTextureOffset += Vector2.down * Time.deltaTime * 200;
    }
}
