using System.Collections;
using UnityEngine;
using System.Linq;

public class RankMgr : MonoBehaviour
{
    public GameObject First;
    public GameObject Second;
    public GameObject Third;

    public GameObject[] Near = new GameObject[7];



    private string id = "";
    private int score = 10;
    private int stage = 1;
    private const int specialRankerLen = 3;

    public void RunRankingList()
    {
        StartCoroutine(RegisterRecord());
    }

    IEnumerator RegisterRecord()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("Stage", stage);
        form.AddField("Score", score);

        postHttpData.PostData("https://busyhuman.pythonanywhere.com/records/", form);
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(ConfirmRecords());
    }

    IEnumerator ConfirmRecords()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/GetHttpData"));
        GetHttpData getHttpData = phd.GetComponent<GetHttpData>();
        getHttpData.GetData("https://busyhuman.pythonanywhere.com/records/?format=json");

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (getHttpData.jsonString == "")
            {
                continue;
            }
            else
            {
                break;
            }

        }


        RecordData[] records = JsonHelper.FromJson<RecordData>(getHttpData.jsonString);

        int recordLen = records.Length, nearLen = Near.Length;

        if (recordLen >= 1)
        {
            First.GetComponent<RenderUserRank>().SetText("1st", records[0].user, records[0].Score.ToString());
        }

        if (recordLen >= 2)
        {
            Second.GetComponent<RenderUserRank>().SetText("2nd", records[1].user, records[1].Score.ToString());
        }

        if (recordLen >= 3)
        {
            Third.GetComponent<RenderUserRank>().SetText("3rd", records[2].user, records[2].Score.ToString());
        }

        for (int i = 0; (i + specialRankerLen < recordLen) && i < nearLen; i++)
        {
            Near[i].GetComponent<RenderUserRank>().SetText((i + specialRankerLen + 1).ToString() + "th", records[i + specialRankerLen].user, records[i + specialRankerLen].Score.ToString());
        }


    }

    // Setter
    public void SetId(string id)
    {
        this.id = id;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void SetStage(int stage)
    {
        this.stage = stage;
    }
}