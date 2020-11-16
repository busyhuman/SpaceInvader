using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaMovingBullet : MonsterBulletBehavior
{
    public Vector3 OriginrPos;
    private float fMoveTick = 0;
    public float Angle = 0;
    public float GravityAccel = 10.0f;
    public void Awake()
    {
        OriginrPos = transform.position;
    }
    protected override void MovingBehavior()
    {

        fMoveTick += Time.deltaTime;
        float SIN = (float)(Math.Sin(30));
        double yPos = -fMoveTick * fMoveTick + 3*fMoveTick;
        float xPos = transform.position.x - MovingSpeed * Time.deltaTime;
        transform.position = new Vector3(xPos, OriginrPos.y + (float)( yPos), 0);

    }
}
