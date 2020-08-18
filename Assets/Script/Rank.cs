using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Rank : MonoBehaviour
{
    // URL
    string url = "https://busyhuman.pythonanywhere.com/users/busyhuman/";

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
                string jsonString = request.downloadHandler.text;
                Debug.Log(jsonString);
                Debug.Log(JsonUtility.FromJson<PlayerData>(jsonString).ID);
            }
        }
    }

    void Start()
    {
        StartCoroutine(getRequest());
    }
}
