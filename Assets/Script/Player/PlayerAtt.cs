using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtt : MonoBehaviour
{
    public AudioClip skillSFX;
    public AudioClip AttSFX;
    protected bool binstantiate = false;
    protected float shootTimer = 0.0f;
    public int att = 1;
    public float shootDelay = 0.2f;
    public float Skilltime = 10;
    public float BulletScale = 1;
    public float bulletSpeed = 10.0f;
    public GameObject Bullet;
    protected CooltimeRender CoolTimer;
    protected AudioSource audioSource;
    public bool bWin = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = AttSFX;
        CoolTimer = GameObject.Find("Cooltime").GetComponent<CooltimeRender>();
        CoolTimer.Activetime = Skilltime;
    }

    public virtual void Skill()
    {
      
    }
    public void TurntoNormalState()
    {
        binstantiate = false;
        audioSource.clip = AttSFX;
    }
    // Update is called once per frame
    void Update()
    {
        if (bWin) return;
        if(CoolTimer.bActivating)
        {
            Skill();
        }
        else
        {
            
            if (shootTimer > shootDelay) //쿨타임이 지났는지와, 공격키인 스페이스가 눌려있는지 검사합니다.
            {
                 
                audioSource.Play();
                GameObject pB = Instantiate(Bullet);
                pB.transform.position = transform.position;
                pB.transform.localScale *= BulletScale;
                pB.GetComponent<Transform>().rotation = (Quaternion.identity); //레이저를 생성해줍니다.
                pB.GetComponent<BulletMove>().player = transform;
                pB.GetComponent<BulletMove>().Speed = bulletSpeed;
                pB.GetComponent<BulletMove>().att = att;
                shootTimer = 0; //쿨타임을 다시 카운트 합니다.
            }
            shootTimer += Time.deltaTime; //쿨타임을 카운트 합니다.
        }

    }
}
