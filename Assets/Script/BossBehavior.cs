using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

enum BossPhase
{
    BOSS_PHASE_APPEAR,
    BOSS_PHASE_IDLE,
    BOSS_PHASE_SHOOT1,
    BOSS_PHASE_SHOOT2,
    BOSS_PHASE_SHOOT3,
    BOSS_PHASE_DYING
}

public class BossBehavior : MonoBehaviour
{
    // 속도
    public float fMoveSpeed = 5.0f;

    // 움직임/공격 타이머
    private float fPhaseTick = 0;
    public float fMoveTick = 0;
    private float fBulletTick = 0;

    // 전투 스펙
    public int iCurrentHP;
    public int iMaxHP = 1500;
    public float fDefense = 1;

    //상태값
    private BossPhase ePhase;

    //임시 위치/방향
    Vector3 vTempPos;
    private float fShootAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        iCurrentHP = iMaxHP;
        ePhase = BossPhase.BOSS_PHASE_APPEAR;
    }
    // Update is called once per frame
    void Update()
    {
        switch (ePhase)
        {
            case BossPhase.BOSS_PHASE_APPEAR:
                Update_Appear();
                break;
            case BossPhase.BOSS_PHASE_IDLE:
                Update_Idle();
                break;
            case BossPhase.BOSS_PHASE_SHOOT1:
                Update_Shoot1();
                break;
            case BossPhase.BOSS_PHASE_SHOOT2:
                Update_Shoot2();
                break;
            case BossPhase.BOSS_PHASE_SHOOT3:
                Update_Shoot3();
                break;
            case BossPhase.BOSS_PHASE_DYING:
                Update_Dying();
                break;
        }
    }
    protected void Shoot()
    {
    }

    protected void Update_Appear()
    {
        transform.Translate(new Vector3(0, -fMoveSpeed * Time.deltaTime * 1.5f, 0), Space.Self);
        fPhaseTick += Time.deltaTime;
        if (fPhaseTick > 1.5f)
        {
            vTempPos = transform.position;
            ePhase = BossPhase.BOSS_PHASE_IDLE;
            fPhaseTick = 0;
        }
    }

    protected void Update_Idle() // 싸인함수그래프대로 둥둥 떠있음
    {
        fPhaseTick += Time.deltaTime;
        fMoveTick += Time.deltaTime * 100;
        float fDegree = 3.14f * fMoveTick / 180.0f;
        float yPos = (float)(vTempPos.y + Math.Sin(fDegree)) / 2.0f;
        Vector3 vPos = new Vector3(vTempPos.x, yPos, vTempPos.z);
        transform.position = vPos;
    }

    protected void Update_Shoot1()
    {
    }

    protected void Update_Shoot2()
    {
    }

    protected void Update_Shoot3()
    {
    }

    protected void Update_Dying()
    {
    }

    public void GetDamage(int iGetDamage)
    {
        iCurrentHP -= (int)(iGetDamage * fDefense);
        if (iCurrentHP <= 0)
        {
            iCurrentHP = 0;
            ePhase = BossPhase.BOSS_PHASE_DYING;
        }
    }

}
