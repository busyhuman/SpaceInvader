using UnityEngine;

struct userInfo
{
    string name;
    int score;
    int rank;
};

public class SortRank : MonoBehaviour
{
    private userInfo[] infos;
    public GameObject First;    // 1등 오브젝트
    public GameObject Second;    // 2등 오브젝트
    public GameObject Third;    // 3등 오브젝트

    private void OnEnable()
    {
        run();
    }

    private void run()
    {

    }

    public void FindUserRank()
    {
        // 유저 랭크 찾기
    }

    public void SetFST()
    {
        // 1등, 2등, 3등   
    }


}
