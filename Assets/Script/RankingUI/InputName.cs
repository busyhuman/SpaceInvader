using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    // Start is called before the first frame update
    private int Score = 0;
    public string name = ""; // 이름
    public InputField inputFiled;
    public GameObject RankingList;
    public RenderUserRank PlayerInfoRenderer;

    private void Awake()
    {
    }
    void Start()
    {
        //inputFiled = GameObject.Find("InputField").GetComponent<InputField>();
        GameObject ScoreMgr = GameObject.Find("UserInfo");
        if (ScoreMgr)
            Score = ScoreMgr.GetComponent<UserInfo>().GetScore();
        inputFiled.ActivateInputField(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RankResults(string _s)
    {
        name = _s;

        /*
          서버 작업
        */

        RankingList.SetActive(true);
        gameObject.SetActive(false);
        PlayerInfoRenderer.SetText(225.ToString(), name, Score.ToString());

    }
}
