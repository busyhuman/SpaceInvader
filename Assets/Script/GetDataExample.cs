using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject gtd;
    string str;
    GetHttpData getHttpData;

    void Awake()
    {
        gtd = Instantiate(gtd) as GameObject;
        getHttpData = gtd.GetComponent<GetHttpData>();
    }

    IEnumerator check()
    {
        getHttpData.GetData("https://busyhuman.pythonanywhere.com/records/?format=json");
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (getHttpData.isDone == false)
            {
                continue;
            }
            str = getHttpData.jsonString;
            RecordData[] records = JsonParser<RecordData>.ParseJsonData(str);

            foreach(RecordData record in records)
            {
                Debug.Log(record.user + "\n" + record.Date + "\n" + record.Stage + "\n" + record.Score);
            }

            break;
        }
    }
    void Start()
    {
        StartCoroutine(check());
    }
}
