using UnityEngine;

public class PlatMove : MoveDown
{
    public bool isForTute = false;
    private void Start()
    {
        speed = 20;
    }

    protected override bool whenDestroy()
    {
        return transform.position.y <= destroyYPos && (isForTute || this.gameObject != GameManager.levelGen.newDist.gameObject);
    }
}
