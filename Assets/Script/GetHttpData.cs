using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GetHttpData : MonoBehaviour
{
    // URL
    string url = "https://busyhuman.pythonanywhere.com/records/";
    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    IEnumerator getRequest()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            // HTTP Request 
            yield return request.SendWebRequest();

            // Check Network Error
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string jsonString = fixJson(request.downloadHandler.text);
                Debug.Log(jsonString);
                RecordData[] record = JsonHelper.FromJson<RecordData>(jsonString);
                Debug.Log(record[0].Date);
            }
        }
    }

    void Start()
    {
        StartCoroutine(getRequest());
    }
}
