﻿using System.Collections;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;

public class PostHttpData : MonoBehaviour
{
    public string DefaultMessage = "No Message";
    public string DefaultErrorMessage = "No ErrorMessage";
    
    public string Message;
    public string ErrorMessage;

    IEnumerator postRequest(string url, WWWForm form)
    {
        UnityWebRequest request = new UnityWebRequest();

        using (request = UnityWebRequest.Post(url, form))
        {
            string token = PlayerPrefs.GetString("Token");
            if (token != "")
            {
                request.SetRequestHeader("Authorization", "Token " + token);
            }

            yield return request.SendWebRequest();

            Message = EngToKor.Translate(request.downloadHandler.text);

            if (request.isNetworkError)
            {
                ErrorMessage = request.error;
                //Debug.LogError("Error: " + Message + "\n" + request.error);
            }

            else if (request.isHttpError)
            {
                ErrorMessage = request.error;
               // Debug.LogError("Error: " + Message + "\n" + request.error);
            }


        }
    
    }
    public void PostData(string url, WWWForm form)
    {
        Message = DefaultMessage;
        ErrorMessage = DefaultErrorMessage;

        StartCoroutine(postRequest(url, form));
        return;
    }

    public string getMessage()
    {
        return Message;
    }

    public string getErrorMessage()
    {
        return ErrorMessage;
    }
}
