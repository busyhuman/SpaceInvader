using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtt : MonoBehaviour
{

    protected float shootTimer = 0.0f;
    public float shootDelay = 0.2f;

    public GameObject Bullet;
    protected CooltimeRender CoolTimer;
    protected AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        CoolTimer = GameObject.Find("Cooltime").GetComponent<CooltimeRender>();

    }

    public virtual void Skill()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
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
                pB.GetComponent<Transform>().rotation = (Quaternion.identity); //레이저를 생성해줍니다.
                pB.GetComponent<BulletMove>().player = transform;
                shootTimer = 0; //쿨타임을 다시 카운트 합니다.
            }
            shootTimer += Time.deltaTime; //쿨타임을 카운트 합니다.
        }

    }
}
