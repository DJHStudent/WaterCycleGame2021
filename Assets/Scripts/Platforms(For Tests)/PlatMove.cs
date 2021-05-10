using UnityEngine;

public class PlatMove : MoveDown
{
    private void Start()
    {
        speed = 20;
    }

    protected override bool whenDestroy()
    {
        if (gameObject.CompareTag("End") || gameObject.CompareTag("Dead"))
        {
            return transform.position.y <= destroyYPos && this.gameObject != GameManager.levelGen.newDist.gameObject;
        }
        else
            return transform.position.y <= destroyYPos;
    }
}
