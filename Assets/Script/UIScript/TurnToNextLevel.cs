using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToNextLevel : MonoBehaviour
{
    public float EndTime = 4;
    public float ElapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime > EndTime)
        {
            GameObject gameMgr = GameObject.Find("GameManager");
            GameObject userInfo = GameObject.Find("UserData");
            if (userInfo)
            {
                UserInfo uf = userInfo.GetComponent<UserInfo>();
                uf.SetStage(uf.GetStage() + 1);
                gameMgr.GetComponent<SceneChanger>().TurnToLoading();
            }
        }
    }
}
