using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAtt : PlayerAtt
{
    public override void Skill()
    {
        if (shootTimer > shootDelay) //쿨타임이 지났는지와, 공격키인 스페이스가 눌려있는지 검사합니다.
        {
            audioSource.clip = skillSFX;
            audioSource.Play();

            Vector3 pos = transform.position;
            audioSource.Play();
            for (int i = 0; i < 2; i++)
            { 
                    GameObject pB = Instantiate(Bullet);
                    pB.transform.position = new Vector3(pos.x + 1.0f, pos.y + i* 1.0f - 0.4f, pos.z);
                    pB.GetComponent<Transform>().rotation = (Quaternion.identity); //레이저를 생성해줍니다.
                    pB.GetComponent<BulletMove>().player = transform;
                    shootTimer = 0; //쿨타임을 다시 카운트 합니다.
            }

        }
        shootTimer += Time.deltaTime; //쿨타임을 카운트 합니다.
    }

}
