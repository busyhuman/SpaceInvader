using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PangSkillBullet : BulletMove
{
    private float elpsedtime = 0;
    // Start is called before the first frame update
    private void Awake()
    {

        GetComponent<Animator>().speed = 0.6f;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameMgr = GameObject.Find("GameManager");
    }
    // Update is called once per frame
    void Update()
    {
        if(bDeath)
        {
            elpsedtime += Time.deltaTime;
            if (elpsedtime > 1.8f)
                GameObject.Destroy(gameObject);
        }
        if(elpsedtime < 1)
        {
            elpsedtime += Time.deltaTime;
          //  Vector3 sc = transform.localScale;
          //  transform.localScale = sc + new Vector3(10, 10, 10) * Time.deltaTime;
          if(elpsedtime > 1)
                GetComponent<Animator>().speed = 0;

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
        if (elpsedtime < 1) return;
        if (collision.tag == "Monster")
        {
            GameMgr.GetComponent<ScoreMgr>().UpdateScore(127);
            GameMgr.GetComponent<SoundMgr>().PlayMonsterDamaged();
            collision.gameObject.GetComponent<MonsterBehavior>().GetDamage(att);
            GetComponent<Animator>().speed = 0.6f;
            bDeath = true;
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossBehavior>().GetDamage(att);
            GetComponent<Animator>().speed = 0.6f;
            bDeath = true;
        }

        if (collision.tag == "BossBullet")
        {
            collision.GetComponent<BossBullet>().DestroyBullet(att);
            GetComponent<Animator>().speed = 0.6f;
            bDeath = true;
        }

    }
}
