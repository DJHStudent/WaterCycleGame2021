using UnityEngine;

public class PlatMove : MoveDown
{
    private void Start()
    {
        speed = 20;
    }

    protected override bool whenDestroy()
    {
        return transform.position.y <= destroyYPos && this.gameObject != GameManager.levelGen.newDist.gameObject;
    }
}
