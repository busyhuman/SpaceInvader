using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : BulletMove
{
    // Start is called before the first frame update
    
    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameMgr = GameObject.Find("GameManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
        Vector3 ScreenPos = cam.WorldToScreenPoint(transform.position);
        if (ScreenPos.x > 1280.0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bDeath) return;
        if (collision.tag == "Monster")
        {
            GameMgr.GetComponent<ScoreMgr>().UpdateScore(127);
            GameMgr.GetComponent<SoundMgr>().PlayMonsterDamaged();
            collision.gameObject.GetComponent<MonsterBehavior>().GetDamage(att);

            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossBehavior>().GetDamage(att);
            Destroy(gameObject);
        }

        if (collision.tag == "BossBullet")
        {
            collision.GetComponent<BossBullet>().DestroyBullet();
            Destroy(gameObject);
        }

        if (collision.tag == "Obstacle" || collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
