using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeGhost : GhostBehavior
{
    public float bAngle = 150;
    public float fMoveTick = 0;
    public float fStartTime = 0;
    private bool bShot = false;
    private bool bAble = true;
    private bool bMove = false;
    public float Xpos = 10;


    // Start is calle
    protected override void AttackBehavior()
    {
        if (!bShot || !bAble) return;
        if (bShot && bAble)
        {
            Instantiate(Bullet, ShootingTransform);
            bShot = false;
            bAble = false;
        }
    }
    protected override void MovingBehavior()
    {
        fMoveTick += Time.deltaTime;
        if (fMoveTick > fStartTime && !bMove) bMove = true;
        if (fMoveTick > 3.3f) bShot = true;
        if (bMove)
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            posX += fMovingSpeed * Time.deltaTime * (float)Math.Cos(bAngle * Math.PI / 180);
            posY += fMovingSpeed * Time.deltaTime * (float)Math.Sin(bAngle * Math.PI / 180);

            if (posY < -10 || posY > 10)
                Destroy(gameObject);
            transform.position = new Vector3(posX, posY, 0);
        }

    }
}
