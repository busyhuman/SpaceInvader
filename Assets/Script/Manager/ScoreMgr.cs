using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMgr : MonoBehaviour
{
    public GameObject ScoreObj;
    public GameObject UserInfo;
    public int iScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        UserInfo = GameObject.Find("UserData");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int iGet)
    {
        iScore += iGet;
        ScoreObj.GetComponent<Score>().iScore = iScore;
        ScoreObj.GetComponent<Score>().StartCoroutine("UpdateScore");
        if(UserInfo)
            UserInfo.GetComponent<UserInfo>().SetScore(iScore);
    }
}
