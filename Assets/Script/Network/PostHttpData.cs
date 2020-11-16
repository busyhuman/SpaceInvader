using System.Collections;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;

public class PostHttpData : MonoBehaviour
{
    public string message = "";

    IEnumerator postRequest(string url, WWWForm form)
    {
        UnityWebRequest request = new UnityWebRequest();

        using (request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.LogError("NetworkError: " + request.error + "\n" + request.downloadHandler.text);
            }

            if (request.isHttpError)
            {
                Debug.LogError("HttpError: " + request.error + "\n" + request.downloadHandler.text);
            }

            message = request.downloadHandler.text;
        }
    
    }
    public void PostData(string url, WWWForm form)
    {
        StartCoroutine(postRequest(url, form));
        return;
    }
}
