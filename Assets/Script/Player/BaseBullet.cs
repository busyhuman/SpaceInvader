using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameMgr;
    public float Speed = 10.0f;
    public bool bDeath = false;
    protected float fDeathTime = 0;
    
    void Awake()
    {
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
        if (transform.localPosition.x > 13.5f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bDeath) return;
        if (collision.tag == "Monster")
        {
            GameMgr.GetComponent<ScoreMgr>().UpdateScore(127);
            GameMgr.GetComponent<SoundMgr>().PlayMonsterDamaged();
            ParticleMgr.GetInstance().CreateDestroyedParticles(collision.gameObject);

            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossBehavior>().GetDamage(5);
            Destroy(gameObject);
        }

        if (collision.tag == "BossBullet")
        {
            collision.GetComponent<BossBullet>().DestroyBullet();
            Destroy(gameObject);
        }

        if (collision.tag == "Barrier")
        {
            Destroy(gameObject);
        }
    }
}
