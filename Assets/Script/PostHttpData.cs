using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PostHttpData : MonoBehaviour
{
    IEnumerator postRequest(string url, WWWForm form)
    {
        UnityWebRequest request = new UnityWebRequest();

        using (request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
        }
    
    }
    public void PostData(string url, WWWForm form)
    {
        StartCoroutine(postRequest(url, form));
        return;
    }
}
