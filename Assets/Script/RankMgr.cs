using System.Collections;
using UnityEngine;
using System.Linq;

public class RankMgr : MonoBehaviour
{
    public GameObject First;
    public GameObject Second;
    public GameObject Third;

    public GameObject[] Near = new GameObject[9];



    private string id = "";
    private int score = 0;
    private int stage = 1;

    public void RunRankingList()
    {
        StartCoroutine(RegisterAccount());
    }


    int Lower_Bound(RecordData[] records, int target)
    {
        int start = 0, end = records.Length - 1;

        while (end > start)
        {
            int mid = (start + end) / 2;

            if (records[mid].Score >= target)
            {
                end = mid;
            }
            else
            {
                start = mid + 1;
            }
        }
        return end;
    }

    IEnumerator RegisterAccount()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = (Instantiate(phd) as GameObject).GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("ID", id);

        postHttpData.PostData("https://busyhuman.pythonanywhere.com/users/", form);
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(RegisterRecord());
    }

    IEnumerator RegisterRecord()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = (Instantiate(phd) as GameObject).GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("user", id);
        form.AddField("Stage", stage);
        form.AddField("Score", score);

        postHttpData.PostData("https://busyhuman.pythonanywhere.com/records/", form);
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(ConfirmRecords());
    }

    IEnumerator ConfirmRecords()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/GetHttpData"));
        GetHttpData getHttpData = (Instantiate(phd) as GameObject).GetComponent<GetHttpData>();
        getHttpData.GetData("https://busyhuman.pythonanywhere.com/records/?format=json");

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (getHttpData.isDone == false)
            {
                continue;
            }
            else
            {
                break;
            }

        }

        RecordData[] records = JsonParser<RecordData>.ParseJsonData(getHttpData.jsonString);

        int recordLen = records.Length;

        if (recordLen >= 1)
        {
            First.GetComponent<RenderUserRank>().SetText("1st", records[recordLen - 1].user, records[recordLen - 1].Score.ToString());
        }

        if (recordLen >= 2)
        {
            Second.GetComponent<RenderUserRank>().SetText("2nd", records[recordLen - 2].user, records[recordLen - 2].Score.ToString());
        }

        if (recordLen >= 3)
        {
            Third.GetComponent<RenderUserRank>().SetText("3rd", records[recordLen - 3].user, records[recordLen - 3].Score.ToString());
        }

        int currentPos = Lower_Bound(records, score), nearLen = Near.Length;
        int maxPos = currentPos + nearLen / 2, minPos = currentPos - nearLen / 2;
        int temp = nearLen / 2;
        int rank = recordLen - currentPos;

        Near[nearLen / 2].GetComponent<RenderUserRank>().SetText((rank).ToString(), this.id, this.score.ToString());

        for (int i = currentPos + 1; i < recordLen && i <= maxPos; i++)
        {
            Near[--temp].GetComponent<RenderUserRank>().SetText((--rank).ToString(), records[i].user, records[i].Score.ToString());
        }

        temp = nearLen / 2;
        rank = recordLen - currentPos;

        for (int i = currentPos - 1; i >= 0 && i >= minPos; i--)
        {
            Near[++temp].GetComponent<RenderUserRank>().SetText((++rank).ToString(), records[i].user, records[i].Score.ToString());
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