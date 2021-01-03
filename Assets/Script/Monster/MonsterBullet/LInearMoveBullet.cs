using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LInearMoveBullet : MonsterBulletBehavior
{
    public float Angle = 0;
    public void Start()
    {
        transform.parent = null;
    }
    protected override void MovingBehavior()
    {

        float posX = transform.position.x;
        float posY = transform.position.y;
        posX += MovingSpeed * Time.deltaTime * (float)Math.Cos(Angle * Math.PI / 180);
        posY += MovingSpeed * Time.deltaTime * (float)Math.Sin(Angle * Math.PI / 180);
        transform.position = new Vector2(posX, posY);
    }
}
