﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MintGhost : GhostBehavior
{
    private float SubTick = 0;
    private float SubLoop = 0.1f;
    int SubNum = 0;

    private bool bMove = true;
    public float Xpos = 10;
    // Start is called before the first frame update
    public void SubInvoke()
    {
        SubNum++;
       Instantiate(Bullet, ShootingTransform);

        if (SubNum == 3)
        {
            CancelInvoke("SubInvoke");
            SubNum = 0;
        }
    }
    protected override void AttackBehavior()
    {
        if (bMove) return;
        ShootingTick += Time.deltaTime;
        if (ShootingTick > ShootingLoop)
        {
            InvokeRepeating("SubInvoke", 0, SubLoop);
            ShootingTick = 0;
        }
    }
    protected override void MovingBehavior()
    {
        if (bMove)
        {
            transform.Translate(Vector3.left * fMovingSpeed * Time.deltaTime);
            if (transform.position.x < Xpos)
                bMove = false;
        }
    }
}
