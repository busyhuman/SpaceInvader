using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogInMgr : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public SceneChanger sc;
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

        yield return StartCoroutine(WaitMessage(postHttpData, 5.0f));

        string msg = postHttpData.getMessage();

        if (postHttpData.getErrorMessage() == postHttpData.DefaultErrorMessage && postHttpData.getMessage() != postHttpData.DefaultMessage)
        {
            TokenMgr.Instance.SetToken(JsonUtility.FromJson<TokenData>(msg).key);
            Debug.Log(msg);
        }
        

        yield return null;
    }

    IEnumerator WaitMessage(PostHttpData postHttpData, float time)
    {
        float WAITING_TIME = time;
        float CURRENT_TIME = 0.0f;
        float CHECK_CYCLE_TIME = 0.5f;
        string defaultMessage = postHttpData.DefaultMessage;

        // time시간동안 메시지가 오는 지 체크
        while (CURRENT_TIME < WAITING_TIME)
        {
            yield return new WaitForSeconds(CHECK_CYCLE_TIME);
            CURRENT_TIME += CHECK_CYCLE_TIME;

            if (postHttpData.getMessage() != defaultMessage)
            {
                break;
            }
        }
        //메인메뉴 전환
        sc.TurnToMainMenu();

        yield return null;
    }
}
