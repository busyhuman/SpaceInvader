using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankMgr : MonoBehaviour
{
    private string id = "";
    private int score = 0;
    private int stage = 1;

    public void RunRankingList()
    {
        StartCoroutine(RegisterAccount());
    }

    IEnumerator RegisterAccount()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = (Instantiate(phd) as GameObject).GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("ID", id);

        postHttpData.PostData("https://busyhuman.pythonanywhere.com/users/", form);
        yield return new WaitForSeconds(1.5f);
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
        yield return StartCoroutine(ShowRecords(records));
    }

    IEnumerator ShowRecords(RecordData[] records)
    {
        foreach (RecordData record in records)
        {
            Debug.Log(record.user + "\n" + record.Date + "\n" + record.Stage + "\n" + record.Score);
        }

        yield return null;
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