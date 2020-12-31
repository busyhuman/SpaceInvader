using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogInMgr : MonoBehaviour
{
    public GameObject Username;
    public GameObject Password;

    public GameObject LoginObj;
    public Text Msg;

    private SceneChanger sc;

    private void Awake()
    {
        sc = GameObject.Find("Main Camera").GetComponent<SceneChanger>();
    }

    public void StartLogin()
    {
        StartCoroutine(LogIn());
    }

    IEnumerator LogIn()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("username", Username.GetComponent<InputField>().text);
        form.AddField("password", Password.GetComponent<InputField>().text);

        postHttpData.PostData(ServerURL.BaseUrl + "rest-auth/login/", form);

        // 서버로부터 메시지 기다림
        yield return StartCoroutine(WaitMessage(postHttpData, 7.0f));
        
        string msg = postHttpData.getMessage();
        if (postHttpData.getErrorMessage() != postHttpData.DefaultErrorMessage)
        {
            Msg.text = msg;
        }

        if (postHttpData.getErrorMessage() == postHttpData.DefaultErrorMessage && postHttpData.getMessage() != postHttpData.DefaultMessage)
        {
            PlayerPrefs.SetString("Token", JsonUtility.FromJson<TokenData>(msg).key);
            Debug.Log(PlayerPrefs.GetString("Token"));

            LoginObj.SetActive(false);
            sc.TurnToMainMenu();
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
