using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PangSkillBullet : BulletMove
{
    private float elpsedtime = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        elpsedtime += Time.deltaTime;
        if(elpsedtime < 1.5f)
        {
            Vector3 sc = transform.localScale;
            transform.localScale = sc + new Vector3(10, 10, 10) * Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            Vector3 ScreenPos = cam.WorldToScreenPoint(transform.position);
            if (ScreenPos.x > 1300.0f) Destroy(gameObject);
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
            collision.GetComponent<BossBehavior>().GetDamage(att);
            GetComponent<Animator>().speed = 1;
        }

        if (collision.tag == "BossBullet")
        {
            collision.GetComponent<BossBullet>().DestroyBullet();
            GetComponent<Animator>().speed = 1;
        }

    }
}
