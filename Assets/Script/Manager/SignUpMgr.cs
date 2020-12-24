using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignUpMgr : MonoBehaviour
{
    public GameObject Username;
    public GameObject Password1;
    public GameObject Password2;
   
    private SceneChanger sc;

    void Start()
    {
        sc = GameObject.Find("Main Camera").GetComponent<SceneChanger>();
    }


    public void StartRegisterAccount()
    {
        StartCoroutine(RegisterAccount());
    }

    IEnumerator RegisterAccount()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();
        
        form.AddField("username", Username.GetComponent<InputField>().text);
        form.AddField("password1", Password1.GetComponent<InputField>().text);
        form.AddField("password2", Password2.GetComponent<InputField>().text);

        postHttpData.PostData(ServerURL.BaseUrl + "rest-auth/registration/", form);

        yield return StartCoroutine(WaitMessage(postHttpData, 5.0f));
        
        if (postHttpData.getErrorMessage() == postHttpData.DefaultErrorMessage && postHttpData.getMessage() != postHttpData.DefaultMessage)
        {
            yield return StartCoroutine(RegisterUserList());

            if (sc != null)
            {
                sc.TurnToMainMenu();
            }
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

    IEnumerator RegisterUserList()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("ID", Username.GetComponent<InputField>().text);

        postHttpData.PostData(ServerURL.BaseUrl + "users/", form);

        yield return new WaitForSeconds(1.0f);
    }
}
