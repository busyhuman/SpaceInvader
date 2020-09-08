using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
public class GetHttpData : MonoBehaviour
{
    public bool isDone = false;
    public string jsonString;

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    IEnumerator GetRequest(string url)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            // Check Network Error
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
                jsonString = request.error;
            }
            else
            {
                jsonString = fixJson(request.downloadHandler.text);
            }

            isDone = true;
        }

    }

    public string GetData(string url)
    {
        StartCoroutine(GetRequest(url));
        return jsonString;
    }
}
