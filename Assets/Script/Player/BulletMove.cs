using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameMgr;
    public float Speed = 10.0f;
    public bool bDeath = false;
    protected float fDeathTime = 0;
    public Transform player;
    public Camera cam;
    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GetComponent<Animator>().speed = 0;
        GameMgr = GameObject.Find("GameManager");
    }
    
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(bDeath)
        {
            fDeathTime += Time.deltaTime;
            if (fDeathTime > 1) Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            Vector3 ScreenPos = cam.WorldToScreenPoint(transform.position);
            if (ScreenPos.x > 1280.0f) Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bDeath) return;
        if(collision.tag == "Monster")
        {
            GameMgr.GetComponent<ScoreMgr>().UpdateScore(127);
            GameMgr.GetComponent<SoundMgr>().PlayMonsterDamaged();
            bDeath = true;
            collision.gameObject.GetComponent<MonsterBehavior>().GetDamage(5);
            GetComponent<Animator>().speed = 1;
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossBehavior>().GetDamage(5);
            bDeath = true;
            GetComponent<Animator>().speed = 1;
        }

        if (collision.tag == "BossBullet")
        {
            collision.GetComponent<BossBullet>().DestroyBullet();
            bDeath = true;
            GetComponent<Animator>().speed = 1;
        }

        if (collision.tag == "Barrier")
        {
            bDeath = true;
            GetComponent<Animator>().speed = 1;
        }
    }
}
