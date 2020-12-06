using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PangAtt : PlayerAtt
{
    private bool binstantiate = false;
    public GameObject skillbullet;
    // Start is called before the first frame update
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
            GameObject obj = Instantiate(skillbullet, transform);
            obj.transform.localPosition = new Vector3(2, 0, 0);
            binstantiate = true;
        }
        else
        {

        }
    }

}
