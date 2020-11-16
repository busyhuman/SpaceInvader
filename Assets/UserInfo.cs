using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    private int iScore = 0;
    public string Name = "";
    private int iRank = 0;
    private int StageData = 0;// 0 : none, 1: 일반몹 1, 2: 보스몹 1
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStage(int num)
    {
        StageData = num;
    }
    public int GetStage() { return StageData; }
    public void SetName(string _Name)
    {
        name = _Name;
    }
    public void SetRank(int _Rank)
    {
        iRank = _Rank;
    }
    public void SetScore(int _Score)
    {
        iScore = _Score;
    }
    public int GetScore()
    {
        return iScore;
    }
}
