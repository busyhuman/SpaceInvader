using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet2 : MonoBehaviour
{
    public float speed = 30.0f;
    public float fShootAngle = 180;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        posX += speed * Time.deltaTime * (float)Math.Cos(fShootAngle * Math.PI / 180);
        posY += speed * Time.deltaTime * (float)Math.Sin(fShootAngle * Math.PI / 180);

        if (posX < -12)
            Destroy(gameObject);

        transform.position = new Vector3(posX, posY, 0);
        if(speed > Time.deltaTime)
            speed -= Time.deltaTime * 1.5f;
    }
}
