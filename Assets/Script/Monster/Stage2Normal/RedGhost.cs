using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhost : GhostBehavior
{
    protected override void MovingBehavior()
    {
        Vector3 dir = player.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.Translate(Vector3.up * Time.deltaTime * fMovingSpeed);
    }
}
