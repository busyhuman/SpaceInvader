using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
//using UnityEngine.XR.WSA.Input;

enum BossShoot
{
    BOSS_SHOOT_NONE,
    BOSS_SHOOT1,
    BOSS_SHOOT2,
    BOSS_SHOOT3
}
enum BossMoveState
{
    BOSS_MOVE_APPEAR,
    BOSS_MOVE_IDLE,
    BOSS_MOVE_SHOOTMOVE,
    BOSS_MOVE_DYING
}

public class BossBehavior : MonoBehaviour
{
    // 속도
    public GameObject[] Bullets;
    public GameObject Player;
    public float fMoveSpeed = 5.0f;
    public GameObject Barrier;
    // 움직임/공격 타이머
    private float fShootTick = 0;
    private float fPatternTick = 0;
    public float fMoveTick = 0;

    private Animator animator;

    // 전투 스펙
    public int iCurrentHP;
    public int iMaxHP = 1500;
    public float fShootSpeed = 1;
    public Transform shootPoint;
    //상태값
    private int iPhase = 1;
    private BossShoot eShootPattern = BossShoot.BOSS_SHOOT_NONE;
    private BossMoveState eMoveState;

    //임시 위치/방향/불렛
    public GameObject Pattern2Bullet;
    public bool bShiled = false;
    bool bShoot = false;
    public int iCurrentMoveIndex = 2;
    int iDestMoveIndex = 0;
    int iBulletNum;
    Vector3 vTempPos;
    Vector3[] vMovingPos;
    private float fShootAngle = 180;
    int iMoveNum = 0;

    public GameObject CurrentBullet;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vMovingPos = new Vector3[22];
        for(int i = 0; i< 22; i++)
        {
            vMovingPos[i] = new Vector3(5.5f, -5.55f + 1.1f * i, 0);
        }
        Player = GameObject.Find("Player");
        iCurrentHP = iMaxHP;
        eMoveState = BossMoveState.BOSS_MOVE_APPEAR;
        eShootPattern = BossShoot.BOSS_SHOOT_NONE ;
    }
    // Update is called once per frame
    void Update()
    {
        switch(eMoveState)
        {
            case BossMoveState.BOSS_MOVE_APPEAR:
                Move_Appear();
                break;
            case BossMoveState.BOSS_MOVE_IDLE:
                Move_Idle();
                break;
            case BossMoveState.BOSS_MOVE_SHOOTMOVE:
                if(eShootPattern == BossShoot.BOSS_SHOOT1)
                    Move_Shoot1();
                break;
            case BossMoveState.BOSS_MOVE_DYING:
                Move_Dying();
                break;
        }
        switch (eShootPattern)
        {     
            case BossShoot.BOSS_SHOOT1:
                ShootPattern1();
                break;
            case BossShoot.BOSS_SHOOT2:
                ShootPattern2();
                break;
            case BossShoot.BOSS_SHOOT3:
                ShootPattern3();
                break;
        }

        fPatternTick += Time.deltaTime;
        
        switch (iPhase)
        {
            case 1:
                if (iCurrentHP < 66)
                {
                    iPhase = 2;
                }
                if (!bShiled)
                    CheckBarrier();
                break;
            case 2:
                if (iCurrentHP < 33)
                {
                    iPhase = 3;
                }
                break;
            case 3:
                if (iCurrentHP < 0)
                {
                    iCurrentHP = 0;
                    eMoveState = BossMoveState.BOSS_MOVE_DYING;
                }
                break;
        }
    }
    protected void SetShootPattern()
    {
        fPatternTick = 0;
        float DistanceToPlayer;
        Vector3 pos = transform.position;
        Vector3 playerPos = Player.GetComponent<Transform>().position;
        DistanceToPlayer = Vector3.Distance(pos, playerPos);

        if(DistanceToPlayer < 4.0f)
        {
            eShootPattern = BossShoot.BOSS_SHOOT3;
            return;
        }

        float Rand = UnityEngine.Random.Range(0, 200.0f);
        if (Rand < 75.0f)
        {
            if(eShootPattern != BossShoot.BOSS_SHOOT1)
                  eShootPattern = BossShoot.BOSS_SHOOT1;
        }
        else
        {
            eShootPattern = BossShoot.BOSS_SHOOT2;
            bShoot = true;
        }
    }
    void CheckBarrier()
    {
        Vector3 PlayerPos = Player.transform.position;
        float distance = Vector3.Distance(PlayerPos, transform.position);

        if(distance < 8)
        {

            Instantiate(Barrier,this.transform);
            bShiled = true;
        }
    }
    protected void Move_Appear()
    {
        transform.Translate(new Vector3(0, -fMoveSpeed * Time.deltaTime * 1.5f, 0), Space.Self);
        fMoveTick += Time.deltaTime;
        if (transform.position.y < -5.0f)
        {
            transform.position = new Vector3(transform.position.x, -5.0f, 0);
            vTempPos = transform.position;
            eMoveState = BossMoveState.BOSS_MOVE_SHOOTMOVE;
            fMoveTick = 0;
            eShootPattern = BossShoot.BOSS_SHOOT1;
            StartCoroutine("Move_Shoot1");
        }
    }

    protected void Move_Idle() // 싸인함수그래프대로 둥둥 떠있음
    {
        fMoveTick += Time.deltaTime * 100;
        float fDegree = 3.14f * fMoveTick / 180.0f;
        float yPos = (float)(vTempPos.y + Mathf.Sin(fDegree)) / 2.0f;
        Vector3 vPos = new Vector3(vTempPos.x, yPos, vTempPos.z);
        transform.position = vPos;
    }

    protected IEnumerator Move_Shoot1()
    {

        Vector3 CurrentPos = transform.position;
        if (iCurrentMoveIndex == iDestMoveIndex)
        {
            Vector3 PlayerPos = Player.transform.position;
            for(int k  =0; k < 18; k++)
            {
                float distance = Math.Abs(vMovingPos[k].y - PlayerPos.y);
                if(distance < 0.275)
                {
                    iDestMoveIndex = k + 4;
                    break;
                }    
            }
           
            iMoveNum++;
            if (iMoveNum % 2 == 0 && fShootTick > 50)
            {
                if(!CurrentBullet)
                {
                    fShootTick = 0;
                    bShoot = true;
                }
            }
            if (iMoveNum % 4 == 0 && fShootTick > 60)
            {
                if (CurrentBullet)
                {
                    fShootTick = 0;
                    CurrentBullet.GetComponent<TetrisBlock>().Launch();
                    animator.SetTrigger("BossLaunch");
                    CurrentBullet = null;
                }

            }
        }
        else if (iCurrentMoveIndex < iDestMoveIndex)
        {
            transform.position = new Vector3(CurrentPos.x, CurrentPos.y + 0.55f, 0);
            iCurrentMoveIndex++;
        }
        else
        {
            transform.position = new Vector3(CurrentPos.x, CurrentPos.y - 0.55f, 0);
            iCurrentMoveIndex--;
        }
        fShootTick += 5;
            yield return new WaitForSeconds(0.2f);
            StartCoroutine("Move_Shoot1");
 }
   
    protected void ShootPattern1()
    {
        if (bShoot == true)
        {
            CreateBullet();
            bShoot = false;
            SetShootPattern();
            fPatternTick = 0;
        }
    }

    protected void ShootPattern2()
    {
        if(bShoot)
        {
            for(int i  = 0; i< 5; i++)
            {
                GameObject pbullet = Instantiate(Pattern2Bullet,transform.position, Quaternion.identity);
                pbullet.GetComponent<BossBullet2>().fShootAngle = 160 + i * 10;
            }
            bShoot = false;
        }
        if (fPatternTick > 8.0f)
            SetShootPattern();
    }

    protected void ShootPattern3()
    {
        if (fPatternTick > 5.0f)
            SetShootPattern();
    }

    private void CreateBullet()
    {
        iBulletNum = UnityEngine.Random.Range(0, Bullets.Length);
        CurrentBullet = Instantiate(Bullets[iBulletNum], shootPoint);
        animator.SetTrigger("BossAttack");
        // bullet.GetComponent<BossBullet>().Initialize(fShootAngle, 10);
    }

    protected void Move_Dying()
    {
    }

    public void GetDamage(int iGetDamage)
    {
        iCurrentHP -= (int)(iGetDamage * fShootSpeed);

    }
}
