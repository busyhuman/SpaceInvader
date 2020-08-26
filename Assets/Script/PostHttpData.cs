using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PostHttpData : MonoBehaviour
{
    // URL
    string url = "https://busyhuman.pythonanywhere.com/records/";
    IEnumerator postRequest()
    {
        UnityWebRequest request = new UnityWebRequest();

        WWWForm form = new WWWForm();
        form.AddField("Stage", "2");
        form.AddField("Score", "13");
        form.AddField("user", "busyhuman");

        using (request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    
    }

    void Start()
    {
        StartCoroutine(postRequest());
    }
}
