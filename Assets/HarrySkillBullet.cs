using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarrySkillBullet : BulletMove
{
    public float Angle = -120;
    void Update()
    {
        if (bDeath)
        {
            fDeathTime += Time.deltaTime;
            if (fDeathTime > 10) Destroy(gameObject);
        }
        else
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            posX -= Speed * Time.deltaTime * (float)Math.Cos(Angle * Math.PI / 180);
            posY -= Speed * Time.deltaTime * (float)Math.Sin(Angle * Math.PI / 180);

            transform.position = new Vector3(posX, posY, 0);

            Vector3 ScreenPos = cam.WorldToScreenPoint(transform.position);
            if (ScreenPos.x > 1280.0f || ScreenPos.x < 0 ||
                ScreenPos.y > 720 || ScreenPos.y < 0) 
            {
                transform.localPosition = new Vector3(UnityEngine.Random.Range(-13, 13), UnityEngine.Random.Range(-13, 13), 0);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bDeath) return;
        if (collision.tag == "Monster")
        {
            GameMgr.GetComponent<ScoreMgr>().UpdateScore(127);
            GameMgr.GetComponent<SoundMgr>().PlayMonsterDamaged();
            collision.gameObject.GetComponent<MonsterBehavior>().GetDamage(att);
            GetComponent<Animator>().speed = 1;
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossBehavior>().GetDamage(5);
            GetComponent<Animator>().speed = 1;
        }

        if (collision.tag == "BossBullet")
        {
            collision.GetComponent<BossBullet>().DestroyBullet();
            GetComponent<Animator>().speed = 1;
        }

    }
}
