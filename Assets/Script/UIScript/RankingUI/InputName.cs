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

    private RankMgr rankMgr;
    void Awake()
    {
        rankMgr = GameObject.Find("RankManager").GetComponent<RankMgr>();
    }
    void Start()
    {
        //inputFiled = GameObject.Find("InputField").GetComponent<InputField>();
        GameObject ScoreMgr = GameObject.Find("UserInfo");
        if (ScoreMgr)
            Score = ScoreMgr.GetComponent<UserInfo>().GetScore();
        inputFiled.ActivateInputField();
    }

    public void RankResults(string _s)
    {
        name = _s;

        RankingList.SetActive(true);

        rankMgr.SetId(name);

        
        rankMgr.SetScore(Score);   // 나중에 랜덤부분 삭제
        rankMgr.RunRankingList();


        gameObject.SetActive(false);
        //  PlayerInfoRenderer.SetText(225.ToString(), name, Score.ToString());
    }
}
