using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarryAtt : PlayerAtt
{
    private bool binstantiate = false;
    public GameObject skillbullet;
    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        CoolTimer = GameObject.Find("Cooltime").GetComponent<CooltimeRender>();
        CoolTimer.Activetime = Skilltime;
    }
    public override void Skill()
    {
        if (!binstantiate)
        {
        Transform cam = GameObject.Find("Main Camera").transform;
            for(int i = 0; i< 20; i ++)
            {
                GameObject obj = Instantiate(skillbullet, cam);
                obj.GetComponent<Transform>().rotation = (Quaternion.identity);
                obj.transform.localPosition = new Vector3(Random.Range(-13,13), Random.Range(-13, 13),15.4f);
            }
            binstantiate = true;
        }
        else
        {

        }
    }

}
