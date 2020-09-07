using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreUnit : MonoBehaviour
{
    public int idigit;
    public GameObject parent;
    private float time = 0.0f;
    public bool bJump = false;
    private float posX, posY ;

    void SetScoreUnit()
    {
        int digit = 0;
        int iScore = parent.GetComponent<Score>().iScore;
       for(int i =0; i<idigit; i++)
        { 
            digit = iScore % 10;
            iScore /= 10;
        }
        this.GetComponent<Text>().text = digit.ToString();
    }
    void Awake()
    {
        posX = GetComponent<RectTransform>().anchoredPosition.x;
        posY = GetComponent<RectTransform>().anchoredPosition.y;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(bJump)
        {
            float height = (time * time * (-9.8f)) + (time * 10);
            GetComponent<RectTransform>().anchoredPosition = new Vector2( posX, posY + height);
            time += Time.deltaTime * 3;
            
            if(time >= 0.2f && time <= 0.23f)
                SetScoreUnit();

            if (height < 0.0f)
            {
                bJump = false;
                time = 0.0f;
                GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
            }


        }
    }
}
