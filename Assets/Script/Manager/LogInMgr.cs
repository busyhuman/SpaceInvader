using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInMgr : MonoBehaviour
{
    public GameObject username;
    public GameObject password;

    public void StartLogin()
    {
        StartCoroutine(LogIn());
    }

    IEnumerator LogIn()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("username", username.GetComponent<InputField>().text);
        form.AddField("password", password.GetComponent<InputField>().text);

        postHttpData.PostData("https://busyhuman.pythonanywhere.com/rest-auth/login/", form);

        yield return null;
    }
}
