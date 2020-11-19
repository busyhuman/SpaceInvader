using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MonsterBehavior : MonoBehaviour
{
    public float fMovingSpeed = 2.0f;
    public int iType = 0;
    public int maxHP = 25;
    public float ShootingTick = 0;
    public float ShootingLoop = 0;
    public float alertDist = 20;
    public GameObject player;

    protected float fAge = 0;
    protected int curHP;
    protected bool bActiveByPlayer = false;
    protected float AttackSpeed = 10;
    public Transform ShootingTransform ;
    public GameObject Bullet;
    protected abstract void AttackBehavior();
    protected abstract void MovingBehavior();

    public void CheckDestroty ()
    {
        fAge += Time.deltaTime;
        if(curHP <= 0)
        {
            ParticleMgr.GetInstance().CreateDestroyedParticles(gameObject);
            GameObject.Destroy(gameObject);
        }
    }
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.Find("Player");
        if (player == null) Debug.Log(player);
        curHP = maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        if(bActiveByPlayer)
        {
            MovingBehavior();
            AttackBehavior();
            CheckDestroty();
        }
        else
        {
          float dist =  transform.position.x - player.transform.position.x;
            if (dist < alertDist)
                bActiveByPlayer = true;
        }
    }
    public void GetDamage( int dam )
    {
        curHP -= dam;
    }
}
