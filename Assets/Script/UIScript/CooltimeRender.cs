using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooltimeRender : MonoBehaviour
{
    public float CurActivetime = 0;
    public float CurCooltime = 0;
    private Material mat;
    bool bCan = false;
    public bool bActivating = false;
    public float Activetime = 10;
    public float Cooltime = 10;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<RawImage>().material;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(bActivating)
        {
            CurActivetime += Time.deltaTime;
            if (CurActivetime > Activetime)
            {
                bActivating = false;
                CurActivetime = 0;
                GameObject.Find("Player").GetComponent<PlayerAtt>().TurntoNormalState();
            }
        }
        else
        {
            CooltimeUpdating();
        }
       

        
        float angle = CurCooltime/Cooltime * 360;
        mat.SetFloat("_TimeSpeed", angle );
    }
    public void CooltimeUpdating()
    {
        if (!bCan)
        {
            CurCooltime += Time.deltaTime;
            if (CurCooltime > Cooltime)
            {
                bCan = true;
            }
        }
    }
    public void UsedSkill()
    {
        if(bCan)
        {
            CurCooltime = 0;
            bCan = false;
            bActivating = true;
        }
    }
}
