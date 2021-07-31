using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDraw : MonoBehaviour
{
    public float radius;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
