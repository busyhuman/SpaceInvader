using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public float ScaleBuff = 1;
    public float bulletScaleBuff = 1;
    public float AttBuff = 1;
    public float MovingSpeedBuff = 1;
    public float BulletSpeedBuff = 1;
    public float AttackSpeedBuff = 1;
    public float SkillCooltimeBuff = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerAtt AttackStat = collision.gameObject.GetComponent<PlayerAtt>();
            PlayerMove MovingStat = collision.gameObject.GetComponent<PlayerMove>();
            collision.transform.localScale *= ScaleBuff;
            MovingStat.moveSpeed *= MovingSpeedBuff;
            AttackStat.BulletScale *= bulletScaleBuff;
            AttackStat.shootDelay *= AttackSpeedBuff;
            AttackStat.bulletSpeed *= BulletSpeedBuff;
            AttackStat.att = (int)(AttackStat.att * AttBuff);

            Destroy(gameObject);
        }
    }
}
