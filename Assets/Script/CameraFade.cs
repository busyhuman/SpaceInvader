using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    public float alpha = 0;
    public bool FadeIn_Out = false;
    public bool bTrigger = false;
    private RawImage Black;    // Start is called before the first frame update
    void Start()
    {
        Black = GetComponent<RawImage>();
        alpha = Black.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if(bTrigger)
        {
            if(FadeIn_Out)
            {
                if (alpha > 0)
                    alpha -= Time.deltaTime;
                else
                {
                    bTrigger = false;
                    FadeIn_Out = false;
                }
                Black.color = new Color(0, 0, 0, alpha );
            }
            else
            {
                if (alpha < 1)
                    alpha += Time.deltaTime;
                else
                {
                    bTrigger = false;
                    FadeIn_Out = true;

                }
               Black.color = new Color(0,0,0, alpha);
            }
        }
    }
    public void SetTrigger()
    {
        if (bTrigger) return;

        bTrigger = true;
        if(FadeIn_Out)
        {
            alpha = 1;
        }
        else
        {
            alpha = 0;

        }
    }
    


}
