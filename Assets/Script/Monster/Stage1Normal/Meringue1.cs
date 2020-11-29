using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Meringue1 : MonsterBehavior
{
    private float SubTick = 0;
    private float SubLoop = 0.2f;
    int SubNum = 0;
    public void Awake()
    {
    }
    protected override void AttackBehavior()
    {
        ShootingTick += Time.deltaTime;
        if(ShootingTick > ShootingLoop)
        {
            InvokeRepeating("SubInvoke", 0,SubLoop);
            ShootingTick = 0;
        }
    }
    protected override void MovingBehavior()
    {
    }
    public void  SubInvoke()
    {
        SubNum++;
        Instantiate(Bullet, ShootingTransform);
        if (SubNum == 2)
        {
            CancelInvoke("SubInvoke");
            SubNum = 0;
        }
    }
}
