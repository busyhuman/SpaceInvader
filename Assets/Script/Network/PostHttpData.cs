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
                Debug.LogError("NetworkError: " + Translator.Translate(request.downloadHandler.text) + "\n" + request.error);
            }

            if (request.isHttpError)
            {
                Debug.LogError("HttpError: " + Translator.Translate(request.downloadHandler.text) + "\n" + request.error);
            }

            message = Translator.Translate(request.downloadHandler.text);
        }
    
    }
    public void PostData(string url, WWWForm form)
    {
        StartCoroutine(postRequest(url, form));
        return;
    }
}
