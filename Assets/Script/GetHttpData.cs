using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
public class GetHttpData : MonoBehaviour
{
    string url;
    public bool isDone = false;
    public string jsonString;

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    IEnumerator GetRequest()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            // Check Network Error
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                jsonString = fixJson(request.downloadHandler.text);
         //       Debug.Log(jsonString);
        //        RecordData[] record = JsonHelper.FromJson<RecordData>(jsonString);
         //       Debug.Log(record[0].Date);
            }
            isDone = true;
        }

    }

    public string GetData(string url)
    {
        this.url = url;
        StartCoroutine(GetRequest());
        return jsonString;
    }
}
