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
    BOSS_SHOOT1, // 테트리스
    BOSS_SHOOT2, // 표창
    BOSS_SHOOT3, // 베리어
    BOSS_SHOOT4, // 광선
    BOSS_SHOOT5, // 몹 소환
    BOSS_SHOOT6 // 돌진
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
    public GameObject DyingParticle;
    public GameObject RayBullet;
    public GameObject TeddyJunior;
    public float fMoveSpeed = 5.0f;
    public GameObject Barrier;
    // 움직임/공격 타이머
    private int iRayCount = 0; // 광선 카운트
    private float fShootTick = 0;
    public float fPatternTick = 0;
    private float fRayTick = 0;
    public float fMoveTick = 0;
    private Vector3 DyingPos;
    private bool bMoving = true;
    private bool bLeft = true;

    private Animator animator;

    //Phace 애니메이션 ㅁ
    public Animator Phcae3Anim;
    // 전투 스펙
    public int iCurrentHP;
    public int iMaxHP = 1000;
    public float fShootSpeed = 1;
    public Transform shootPoint;
    //상태값
    public int iPhase = 1;
    private BossShoot eShootPattern = BossShoot.BOSS_SHOOT_NONE;
    private BossMoveState eMoveState;

    //임시 위치/방향/불렛
    public GameObject Pattern2Bullet;
    private bool bShiled = false;
    bool bShoot = false;
    public int iCurrentMoveIndex = 20;
    int iDestMoveIndex = 20;
    int iBulletNum;
    Vector3 vTempPos;
    Vector3[] vMovingPos;
    private float fShootAngle = 180;
    int iMoveNum = 0;
    private BossSound sound;
   
    public GameObject CurrentBullet;

    // Start is called before the first frame update
    void Start()
    {
        GameObject userInfo = GameObject.Find("UserData");
        if(userInfo)
        {
            userInfo.GetComponent<UserInfo>().SetStage(2);
        }
        sound = gameObject.GetComponent<BossSound>();
        animator = GetComponent<Animator>();
        vMovingPos = new Vector3[26];
        for (int i = 0; i < 26; i++)
        {
            vMovingPos[i] = new Vector3(5.5f, -4.6f + 0.35f * i, 0);
        }
        Player = GameObject.Find("Player");
        iCurrentHP = iMaxHP;
        eMoveState = BossMoveState.BOSS_MOVE_APPEAR;
        eShootPattern = BossShoot.BOSS_SHOOT_NONE;
    }
    // Update is called once per frame
    void Update()
    {
        
        switch (eMoveState)
        {
            case BossMoveState.BOSS_MOVE_APPEAR:
                Move_Appear();

                break;
            case BossMoveState.BOSS_MOVE_IDLE:
                Move_Idle();

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
                    case BossShoot.BOSS_SHOOT4:
                        ShootPattern4();
                        break;
                    case BossShoot.BOSS_SHOOT5:
                        ShootPattern5();
                        break;
                }
                break;
            case BossMoveState.BOSS_MOVE_SHOOTMOVE:
                fPatternTick += Time.deltaTime;
                if(Player.GetComponent<PlayerMove>().iDieState == 0)
                {
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
                        case BossShoot.BOSS_SHOOT4:
                            ShootPattern4();
                            break;
                        case BossShoot.BOSS_SHOOT5:
                            ShootPattern5();
                            break;
                        case BossShoot.BOSS_SHOOT6:
                            ShootPattern6();
                            break;
                    }

                }
                break;
            case BossMoveState.BOSS_MOVE_DYING:
                Move_Dying();
                break;
        }


        switch (iPhase)
        {
           
            case 1:
                if ((float)(iCurrentHP) / (float)(iMaxHP) < 0.8f)
                {
                    iPhase = 2;

                    animator.SetTrigger("Phace2");
                }
                break;
            case 2:
                if ((float)(iCurrentHP) / (float)(iMaxHP) < 0.3f)
                {
                    iPhase = 3;

                    animator.SetTrigger("Phace3");
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
                if (!bShiled && eShootPattern != BossShoot.BOSS_SHOOT6)
                    CheckBarrier();
    }

    public bool GetIsRushing()
    {
        if (eShootPattern == BossShoot.BOSS_SHOOT6 && bLeft)
            return true;
        else return false;
    }
    public void SetShild(bool b)
    {
        bShiled = b;
    }
    private void SetShootPatternByPhase(float[] percentage)
    {
        float Rand = UnityEngine.Random.Range(0, 100.0f);
        if (Rand < percentage[0])
        {
            if (eShootPattern != BossShoot.BOSS_SHOOT1)
                eShootPattern = BossShoot.BOSS_SHOOT1;
        }
        else if (Rand < percentage[1])
        {
            eShootPattern = BossShoot.BOSS_SHOOT2;
            bShoot = true;
        }
        else if (Rand < percentage[2])
        {
            eShootPattern = BossShoot.BOSS_SHOOT4;
            bShoot = false;
        }
        else if(Rand < percentage[3])
        {
            eShootPattern = BossShoot.BOSS_SHOOT5;
            bShoot = true;
        }
        else
        {
            eShootPattern = BossShoot.BOSS_SHOOT6;
            animator.SetTrigger("Rush");
            //bShoot = true;
        }

    }
    protected void SetShootPattern()
    {
        fPatternTick = 0;
        float DistanceToPlayer;
        Vector3 pos = transform.position;
        Vector3 playerPos = Player.GetComponent<Transform>().position;
        DistanceToPlayer = Vector3.Distance(pos, playerPos);

        if (DistanceToPlayer < 4.0f)
        {
            eShootPattern = BossShoot.BOSS_SHOOT3;
            return;
        }

        float[] percentage = new float[5];
        // {테트리스, 표창, 광선, 몹 소환, 돌진}
        switch (iPhase)
        {
            case 1:
                percentage = new float[5] { 70.0f, 100.0f, 0.0f, 0.0f, 0.0f };
                break;
            case 2:
                percentage = new float[5] { 50.0f, 70.0f, 90.0f, 100.0f, 0.0f };
                break;
            case 3:
                percentage = new float[5] { 40.0f, 60.0f, 80.0f, 90.0f, 100.0f };
                break;
        }
        SetShootPatternByPhase(percentage);
        if (CurrentBullet && eShootPattern != BossShoot.BOSS_SHOOT1) GameObject.Destroy(CurrentBullet);
    }
    void CheckBarrier()
    {
        Vector3 PlayerPos = Player.transform.position;
        float distance = Vector3.Distance(PlayerPos, transform.position);

        if (distance < 8)
        {
            sound.PlayBubble();
            Instantiate(Barrier, this.transform);
            bShiled = true;
        }
    }
    protected void Move_Appear()
    {
        transform.Translate(new Vector3(0, -fMoveSpeed * Time.deltaTime * 1.5f, 0), Space.Self);
        fMoveTick += Time.deltaTime;
        if (transform.position.y < vMovingPos[iCurrentMoveIndex].y)
        {
            transform.position = new Vector3(transform.position.x, vMovingPos[iCurrentMoveIndex].y, 0);
            vTempPos = transform.position;
            eMoveState = BossMoveState.BOSS_MOVE_SHOOTMOVE;
            fMoveTick = 0;
            CheckPlayerPosRow();
            SetShootPattern();
            StartCoroutine("Move");
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
    protected void CheckPlayerPosRow()
    {
        Vector3 CurrentPos = transform.position;
        Vector3 PlayerPos = Player.transform.position;
        for (int k = 0; k < 26; k++)
        {
            float distance = Math.Abs(vMovingPos[k].y - PlayerPos.y);
            if (distance < 0.127)
            {
                iDestMoveIndex = k;
                break;
            }
        }

    }
    protected IEnumerator Move()
    {

        Vector3 CurrentPos = transform.position;
        bool bNormalMove = eShootPattern != BossShoot.BOSS_SHOOT6 || bMoving;
        
         if (iCurrentMoveIndex == iDestMoveIndex)
        {
            CheckPlayerPosRow();
            if (bMoving && eShootPattern == BossShoot.BOSS_SHOOT6)
                bMoving = false;

            iMoveNum++;
            if (iMoveNum % 2 == 0 && fShootTick > 50)
            {
                if (!CurrentBullet && eShootPattern == BossShoot.BOSS_SHOOT1)
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
                    animator.SetTrigger("Attack");

                    sound.PlaySkill1();
                    CurrentBullet = null;
                }

            }
        }

        else if (iCurrentMoveIndex < iDestMoveIndex && bNormalMove)
        {
            iCurrentMoveIndex++;
            if (iCurrentMoveIndex > 25) iCurrentMoveIndex = 25;
        }
        else if(bNormalMove)
        {
            iCurrentMoveIndex--;
            if (iCurrentMoveIndex <0) iCurrentMoveIndex = 0;
        }

        if (bNormalMove)
            transform.position = new Vector3(CurrentPos.x, vMovingPos[iCurrentMoveIndex].y, 0);

        if(eShootPattern == BossShoot.BOSS_SHOOT1)
            fShootTick += 5;



        yield return new WaitForSeconds(0.15f);
        if(eMoveState != BossMoveState.BOSS_MOVE_DYING)
             StartCoroutine("Move");
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
        if (bShoot)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject pbullet = Instantiate(Pattern2Bullet, transform.position, Quaternion.identity);
                pbullet.GetComponent<BossBullet2>().fShootAngle = 160 + i * 15;
            }
            sound.PlaySkill2();
            bShoot = false;
        }
        if (fPatternTick > 5.0f)
            SetShootPattern();
    }

    protected void ShootPattern3()
    {
        if (fPatternTick > 5.0f)
            SetShootPattern();
    }
    private void ShootPattern4()
    {
        if(bShoot && iRayCount < 3)
        {
            Instantiate(RayBullet, transform);
            bShoot = false;
            iRayCount += 1;
        }
        else
        {
            fRayTick += Time.deltaTime;
            if(fRayTick > 1)
            {
                fRayTick = 0;
                bShoot = true;

            }
        }

        if (iRayCount > 2)
        {
            bShoot = false;
            if( fPatternTick > 3.9f)
            {

                SetShootPattern();
                iRayCount = 0;
            }
        }

    }

    private void ShootPattern5()
    { 
        if(bShoot)
        {
            animator.SetTrigger("CallJunior");
            for (int i = 0; i < 5; i++)
                Instantiate(TeddyJunior, transform.position, Quaternion.identity);
              
            sound.PlaySkill2();
            bShoot = false;
        }
        if (fPatternTick > 5.0f)
            SetShootPattern();
    }

    private void ShootPattern6()
    {
        if (!bMoving)
        {
            if (bLeft)
            {
                transform.Translate(Vector3.left * Time.deltaTime * 9);
                if (transform.position.x < -12)
                    bLeft = false;
            }
            else
            {

                transform.Translate(Vector3.right * Time.deltaTime * 9);
                if (transform.position.x > 9.42)
                {
                    bLeft = true;
                    transform.position = new Vector3(9.42f, transform.position.y, transform.position.z);
                    SetShootPattern();
                }
            }
        }
    }

    private void CreateBullet()
    {
        iBulletNum = UnityEngine.Random.Range(0, Bullets.Length);
        CurrentBullet = Instantiate(Bullets[iBulletNum], shootPoint);
        // bullet.GetComponent<BossBullet>().Initialize(fShootAngle, 10);
    }

    protected void Move_Dying()
    {
        fMoveTick += Time.deltaTime;
        float yPos = -2.0f * fMoveTick * fMoveTick + 10 * fMoveTick;
        transform.position = new Vector3(DyingPos.x, DyingPos.y + yPos, 0);
        if(fMoveTick * 40 < 180)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, fMoveTick*40));
        if (transform.position.y < -10)
            Player.GetComponent<PlayerMove>().TurnToWin2Mode();
    }

    public void GetDamage(int iGetDamage)
    {
        if (eMoveState == BossMoveState.BOSS_MOVE_DYING || eMoveState == BossMoveState.BOSS_MOVE_APPEAR) return;
        iCurrentHP -= iGetDamage;
        if (iCurrentHP <= 0)
        {
            sound.PlayHurt();
            eMoveState = BossMoveState.BOSS_MOVE_DYING;
            DyingParticle.SetActive(true);
            DyingPos = transform.position;
            fMoveTick = 0;
            Player.GetComponent<PlayerMove>().TurnToWinMode();
            animator.SetTrigger("DIe");
            if (CurrentBullet) Destroy(CurrentBullet);
        }

    }
}
