using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameMgr;
    public float Speed = 10.0f;
   
    void Awake()
    {
        GameMgr = GameObject.Find("GameManager");
    }
    
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
        if (transform.position.x > 10.5f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
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
