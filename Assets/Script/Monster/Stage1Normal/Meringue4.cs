using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meringue4 : MonsterBehavior
{
    protected override void AttackBehavior()
    {
        ShootingTick += Time.deltaTime;
        if (ShootingTick > ShootingLoop)
        {
            Instantiate(Bullet, ShootingTransform);
            ShootingTick = 0;
        }

    }
    protected override void MovingBehavior()
    {
    }
}
