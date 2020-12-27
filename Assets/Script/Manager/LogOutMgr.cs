using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class LogOutMgr : MonoBehaviour
{
    public GameObject SettingUIObj;

    public void StartLogOut()
    {
        StartCoroutine(LogOut());
    }

    IEnumerator LogOut()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        postHttpData.PostData(ServerURL.BaseUrl + "rest-auth/logout/", form);

        // 서버로부터 메시지 기다림
        yield return StartCoroutine(WaitMessage(postHttpData, 5.0f));

        if (postHttpData.getErrorMessage() == postHttpData.DefaultErrorMessage && postHttpData.getMessage() != postHttpData.DefaultMessage)
        {
            Debug.Log("Token(Before): " + PlayerPrefs.GetString("Token"));
            PlayerPrefs.SetString("Token", "");
            Debug.Log("Token(After): " + PlayerPrefs.GetString("Token"));
        }

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


        yield return null;
    }
}
