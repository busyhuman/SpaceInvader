using System.Collections;
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

        yield return StartCoroutine(WaitToken(postHttpData));

        string msg = postHttpData.getMessage();
        if(postHttpData.getErrorMessage() == "")
        {
            TokenMgr.Instance.SetToken(JsonUtility.FromJson<TokenData>(msg).key);
            Debug.Log(msg);
        }

        yield return null;
    }

    IEnumerator WaitToken(PostHttpData postHttpData)
    {
        int WAITING_TIME = 50;
        int CURRENT_TIME = 0;

        // 5초 동안 메시지가 오지 않으면 탈출
        while (CURRENT_TIME++ < WAITING_TIME)
        {
            yield return new WaitForSeconds(0.1f);

            if (postHttpData.getMessage() != "")
            {
                break;
            }
        }
    }
}
