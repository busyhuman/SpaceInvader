using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooltimeRender : MonoBehaviour
{
    public float time = 0;
    private Material mat;
    bool bCan = false;
    public float Cooltime = 10;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<RawImage>().material;

    }

    // Update is called once per frame
    void Update()
    {
        if (!bCan)
        {
            time += Time.deltaTime;
            if (time > Cooltime)
            {
                bCan = true;
            }
        }

        
        float angle = time/Cooltime * 360;
        mat.SetFloat("_TimeSpeed", angle );
    }
    public void UsedSkill()
    {
        if(bCan)
        {
            time = 0;
            bCan = false;

        }
    }
}
